
using Dapper;
using Domain;
using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.TypeHandler;
using System;
using System.Collections.Generic;

namespace CRUD.ContainerBuilder
{
    public class Builder
    {
        public Builder(IServiceCollection services)
        {

            SqlMapper.AddTypeHandler(typeof(string[]), new SqlMapperTypeHandler());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAccountManager, AccountManager>();

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();



        }
    }
}
