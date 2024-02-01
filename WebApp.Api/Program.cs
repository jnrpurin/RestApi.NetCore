using WebApp.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddCors(opt =>
//    opt.AddDefaultPolicy(policy =>
//        {
//            policy.AllowAnyOrigin();
//            policy.AllowAnyHeader();
//            policy.AllowAnyMethod();
//        })
//    );
builder.Services.AddSwaggerGen();

//builder.Services.AddBussinessServices();
builder.Services.AddSqlServerDbSession(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
