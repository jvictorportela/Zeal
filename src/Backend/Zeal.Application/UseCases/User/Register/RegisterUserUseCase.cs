﻿using AutoMapper;
using FluentValidation.Results;
using Zeal.Communication.Requests.User;
using Zeal.Communication.Responses.User;
using Zeal.Domain.Repositories;
using Zeal.Domain.Repositories.User;
using Zeal.Domain.Security.Cryptography;
using Zeal.Domain.Security.Tokens;
using Zeal.Exceptions;
using Zeal.Exceptions.ExceptionsBase;

namespace Zeal.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository, 
        IUserReadOnlyRepository readOnlyRepository,
        IMapper mapper,
        IPasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisterUserjson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisterUserjson
        {
            Name = request.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}