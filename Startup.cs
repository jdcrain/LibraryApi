using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Extensions;
using JsonApiDotNetCore.Models;
using LibraryApi.Domain.Authors;
using LibraryApi.Domain.Books;
using LibraryApi.Domain.Reviews;
using LibraryApi.Domain.Users;
using LibraryApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AppDbContext>();
            services.AddJsonApi<AppDbContext>(options => 
            {
                options.ValidateModelState = true;
            });

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT_SECRET"])); // create a signing key so that we know we created the JWT
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            
            // add signing key and credentials as singletons to use the same values across all requests
            services.AddSingleton(signingKey);
            services.AddSingleton(creds);


            services.AddCors();

            // add scoped creates one instance per request
            services.AddScoped<IEntityRepository<Author>, AuthorsRepository>(); // register author repository to override query
            services.AddScoped<IEntityRepository<Book>, BooksRepository>(); // register book repository to override query
            services.AddScoped<IEntityRepository<Review>, ReviewsRepository>(); // register review repository to override query

            services.AddScoped<ResourceDefinition<User>, UserResource>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // configre CORS for all origins, all methods (GET, POST, PUT, etc.), and allow any headers
            app.UseCors(a => a.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            //app.UseHttpsRedirection(); // want for production but not development
            app.UseMvc();
            app.UseJsonApi();

            // Generate new secret key for signing and print to console. Used in config file
            //var crypto = new System.Security.Cryptography.RNGCryptoServiceProvider(); // create cryptographic number generator for better random numbers
            //var bytes = new byte[48];
            //crypto.GetBytes(bytes);
            //Console.WriteLine(Convert.ToBase64String(bytes));
        }
    }
}
