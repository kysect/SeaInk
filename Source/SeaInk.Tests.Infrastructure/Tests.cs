using System;
using Infrastructure.APIs;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SeaInk.Core.APIs;

namespace SeaInk.Tests.Infrastructure
{
    public class Tests
    {
        private IServiceProvider _provider;
        private ITestUniversitySystemApi _api;

        [SetUp]
        public void Setup()
        {
            _provider = new ServiceCollection()
                .AddTestServices()
                .BuildServiceProvider();

            if (_provider.GetRequiredService<IUniversitySystemApi>() is not ITestUniversitySystemApi api)
                throw new ArgumentException("Using not testing API in testing");

            _api = api;
        }

        [Test]
        public void Test1()
            => Assert.Pass();
    }
}