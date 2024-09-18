﻿using FinanceFlow.Domain.Repositories.Users;
using Moq;

namespace commonTestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}