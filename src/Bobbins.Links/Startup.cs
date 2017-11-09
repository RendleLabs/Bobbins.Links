using AutoMapper;
using Bobbins.Links.Data;
using Bobbins.Links.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bobbins.Links
{
    [UsedImplicitly]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [PublicAPI]
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<LinkContext>(o => { o.UseNpgsql(Configuration.GetConnectionString("Links")); });
            
            services.AddSingleton<IMapper, Mapper>(_ =>
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Link, LinkDto>();
                    cfg.CreateMap<LinkDto, Link>();
                    cfg.CreateMap<Vote, VoteDto>();
                    cfg.CreateMap<VoteDto, Vote>();
                });
                return new Mapper(mapperConfig);
            });
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
