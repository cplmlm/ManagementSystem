using Autofac.Extensions.DependencyInjection;
using Autofac;
using Extensions.AutoMapper;
using Extensions.ServiceExtensions;
using ManagementSystem.Extensions;
using ManagementSystem.Common.Seed;
using ManagementSystem.Common.Core;
using ManagementSystem.Common;
using ManagementSystem.Filter;
using ManagementSystem.Common.Helper;
using ManagementSystem.Extensions.HostedService;
using Microsoft.Extensions.FileProviders;
using NLog.Web;
using ManagementSystem.Web;


var builder = WebApplication.CreateBuilder(args);
//配置host与容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModuleRegister());
    });

// 2、配置服务
//builder.Services.AutoRegistryService(ServiceLifetime.Transient);
//builder.Services.BatchRegisterServiceByInterface(typeof(ITransientInterface));
builder.Services.AddSingleton(new AppSettings(builder.Configuration));
builder.Services.AddControllers();
builder.Services.AddCacheSetup();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextSetup();
builder.Services.AddAuthorizationSetup();
builder.Services.AddAuthentication_JWTSetup();
builder.Services.AddAutoMapper(typeof(CustomProfile));
builder.Services.AddSqlsugarSetup();
builder.Services.AddSwaggerSetup();
builder.Services.AddHostedService<SeedDataHostedService>(); 
builder.Services.AddScoped<DBSeed>();
builder.Services.AddScoped<MyContext>();
builder.Services.AddHttpClient();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultWrapperFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(options =>
{
    //将long类型转为string
    options.SerializerSettings.Converters.Add(new NumberConverter(NumberConverterShip.Int64));
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//设置可访问的文件夹路径
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resource")),
    RequestPath = "/Resource"
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
