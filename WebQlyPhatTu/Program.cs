using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Sevices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddTransient<IGetInfoFromToken, GetInfoFromTokenService>();
builder.Services.AddTransient<IChuaServices, ChuaServices>();
builder.Services.AddTransient<IPhatTuServices, PhatTuServices>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var secretKeyBytes = Encoding.UTF8.GetBytes(builder.Configuration["Appsettings:SecretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //Tu Cap Token
        ValidateIssuer = false,
        ValidateAudience = false,

        //Ky vao Token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
        ClockSkew = TimeSpan.Zero
    };
});

// Tạo phân quyền cho API
builder.Services.AddAuthorization(option =>
{
    //option.AddPolicy("ADMINANDMEMBER", policy =>
    //    policy.RequireRole(
    //         "ADMIN", "MEMBER" 
    //        )
    //);
    option.AddPolicy("ADMINANDMEMBER", policy =>
        policy.RequireClaim(ClaimTypes.Role,
             "ADMIN", "MEMBER"
            )
    );
    option.AddPolicy("MEMBER", policy => policy.RequireClaim(
        ClaimTypes.Role, "MEMBER"
        ));
    option.AddPolicy("ADMIN", policy => policy.RequireClaim(
     ClaimTypes.Role, "ADMIN"
     ));
    option.AddPolicy("TRUTRI", policy =>
    policy.RequireClaim(ClaimTypes.Role, "TRUTRI")
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
