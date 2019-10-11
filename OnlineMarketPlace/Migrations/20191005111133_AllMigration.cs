using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarketPlace.Migrations
{
    public partial class AllMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    RegisteredDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    SpecialUser = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    DefinedByUserId = table.Column<string>(nullable: true),
                    Gendre = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_DefinedByUserId",
                        column: x => x.DefinedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorName = table.Column<string>(nullable: true),
                    ColorLatinName = table.Column<string>(nullable: true),
                    HexCode = table.Column<string>(nullable: true),
                    Rgba = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    RangeIp = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    Map = table.Column<byte[]>(nullable: true),
                    Flag = table.Column<byte[]>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Population = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RatioPrice = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginFailure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ip = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Mobile = table.Column<bool>(nullable: false),
                    Os = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    EnteredUserName = table.Column<string>(nullable: true),
                    EnteredPassword = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginFailure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Icon = table.Column<byte[]>(nullable: true),
                    IconPath = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenResulation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    LatinName = table.Column<string>(nullable: true),
                    Dimensions = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenResulation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Size = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Texture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextureName = table.Column<string>(nullable: true),
                    TextureLatinName = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    ImageIcon = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalFeatures_AdditionalFeatures_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AdditionalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalFeatures_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    LatinTitle = table.Column<string>(nullable: true),
                    UserRoleId = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ParentId = table.Column<int>(nullable: true),
                    IconPath = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminMenu_AdminMenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AdminMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminMenu_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    RegdDateTime = table.Column<DateTime>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ApprovedByUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactUs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    MultiProducts = table.Column<bool>(nullable: false, defaultValue: true),
                    PolicyJson = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    GeneratedCode = table.Column<bool>(nullable: false, defaultValue: false),
                    CouponCode = table.Column<string>(nullable: true),
                    ApplyOnAllGroups = table.Column<bool>(nullable: false, defaultValue: false),
                    ForSpecialUsers = table.Column<bool>(nullable: false, defaultValue: false),
                    ForSpecialUser = table.Column<bool>(nullable: false, defaultValue: true),
                    SpecialUserId = table.Column<string>(nullable: true),
                    AutoSet = table.Column<bool>(nullable: false, defaultValue: false),
                    ApplyOnTag = table.Column<bool>(nullable: false, defaultValue: false),
                    AutoIndex = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupon_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Coupon_AspNetUsers_SpecialUserId",
                        column: x => x.SpecialUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Coupon_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuaranteeProvider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CatalogPath = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranteeProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuaranteeProvider_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    AliasName = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchFilters_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    BaseCurrencyId = table.Column<int>(nullable: true),
                    IsStatusActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsMarketPlace = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    AdminEmail = table.Column<string>(nullable: true),
                    AdminEmailPassword = table.Column<string>(nullable: true),
                    EmailPort = table.Column<string>(nullable: true),
                    EmailProtocol = table.Column<string>(nullable: true),
                    EmailServiceProvider = table.Column<string>(nullable: true),
                    SMSUsername = table.Column<string>(nullable: true),
                    SMSPassword = table.Column<string>(nullable: true),
                    SMSApiAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setting_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    AllowForGuest = table.Column<bool>(nullable: true),
                    AllowChangeCountry = table.Column<bool>(nullable: true),
                    AllowChangeProvince = table.Column<bool>(nullable: true),
                    AllowChangeCity = table.Column<bool>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Policy = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: true),
                    DynamicPrice = table.Column<decimal>(type: "Money", nullable: true),
                    OnlyPartner = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingMethod_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteGeneralInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopTitle = table.Column<string>(nullable: true),
                    LatinShopTitle = table.Column<string>(nullable: true),
                    MetaTagJson = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DescriptionForFooter = table.Column<string>(nullable: true),
                    CanonicalTag = table.Column<string>(nullable: true),
                    CanonicalTagDescription = table.Column<string>(nullable: true),
                    PrimarySiteLogo = table.Column<byte[]>(nullable: true),
                    SecondarySiteLogo = table.Column<byte[]>(nullable: true),
                    PrimarySiteLogoPath = table.Column<string>(nullable: true),
                    SecondarySiteLogoPath = table.Column<string>(nullable: true),
                    ConnectedLinks = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Motto = table.Column<string>(nullable: true),
                    PublicEmail = table.Column<string>(nullable: true),
                    BusinessEmail = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    PhoneNumbersList = table.Column<string>(nullable: true),
                    AddressesList = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Signature = table.Column<string>(nullable: true),
                    SetForMainPage = table.Column<bool>(nullable: false, defaultValue: true),
                    LicensesListName = table.Column<string>(nullable: true),
                    AchievedAwards = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteGeneralInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteGeneralInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Comment = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ImageThumbnail = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    LogTime = table.Column<TimeSpan>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    MobileOs = table.Column<bool>(nullable: false),
                    SpentTime = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLog_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserModified",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModifedByUserId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    LastUserBackupJson = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModified_AspNetUsers_ModifedByUserId",
                        column: x => x.ModifedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserModified_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserReported",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ReportedByUserId = table.Column<string>(nullable: true),
                    Determination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReported", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReported_AspNetUsers_ReportedByUserId",
                        column: x => x.ReportedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserReported_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VerificationCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    SendToPhoneNumber = table.Column<bool>(nullable: true),
                    SendToEmail = table.Column<bool>(nullable: true),
                    GeneratedCode = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    StartDateTime = table.Column<DateTime>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ReasonOfGenerate = table.Column<string>(nullable: true),
                    AutoGenerate = table.Column<bool>(nullable: true, defaultValue: false),
                    RequestedByAdmin = table.Column<bool>(nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationCode_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNo = table.Column<string>(nullable: true),
                    CardNo = table.Column<string>(nullable: true),
                    ShebaNo = table.Column<string>(nullable: true),
                    Posset = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    OnlineService = table.Column<bool>(nullable: false, defaultValue: false),
                    IsPublic = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    BankId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    ContinentId = table.Column<int>(nullable: true),
                    Map = table.Column<byte[]>(nullable: true),
                    Flag = table.Column<byte[]>(nullable: true),
                    OfficialLanguageFirst = table.Column<string>(nullable: true),
                    OfficialLanguageSecond = table.Column<string>(nullable: true),
                    Population = table.Column<float>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Motto = table.Column<string>(nullable: true),
                    Emblem = table.Column<byte[]>(nullable: true),
                    Capital = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    Gdp = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    CallingCode = table.Column<int>(nullable: true),
                    InternetTld = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    RangeIp = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CountryAlphaCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    LatinTitle = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Deadline = table.Column<bool>(nullable: false, defaultValue: false),
                    StartDateTime = table.Column<DateTime>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Comment = table.Column<string>(nullable: true),
                    CouponId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    AliasName = table.Column<string>(nullable: true),
                    TitleAltName = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    ImageIcon = table.Column<string>(nullable: true),
                    Image1 = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ImageForMenu = table.Column<byte[]>(nullable: true),
                    ImageForMenuPath = table.Column<string>(nullable: true),
                    FieldId = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    ConnectedLink = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    TotalVisit = table.Column<int>(nullable: true),
                    OrderedCount = table.Column<int>(nullable: true),
                    ShowInMainPage = table.Column<bool>(nullable: false, defaultValue: true),
                    Priority = table.Column<int>(nullable: true),
                    ShowInMenu = table.Column<bool>(nullable: false, defaultValue: true),
                    ShowInFooter = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Link = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RelatedProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoGallery_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoGallery_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockedUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserLogId = table.Column<int>(nullable: true),
                    Block = table.Column<bool>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    AutoBlock = table.Column<bool>(nullable: true),
                    ByUserId = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    LoginFailureId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockedUser_AspNetUsers_ByUserId",
                        column: x => x.ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockedUser_LoginFailure_LoginFailureId",
                        column: x => x.LoginFailureId,
                        principalTable: "LoginFailure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockedUser_UserLog_UserLogId",
                        column: x => x.UserLogId,
                        principalTable: "UserLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchedText = table.Column<string>(nullable: true),
                    UserLogId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchedItems_UserLog_UserLogId",
                        column: x => x.UserLogId,
                        principalTable: "UserLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBehaviorTracking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserLogId = table.Column<int>(nullable: true),
                    MouseOrTouchBehavior = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBehaviorTracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBehaviorTracking_UserLog_UserLogId",
                        column: x => x.UserLogId,
                        principalTable: "UserLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BankAccountId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    Pending = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seller_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seller_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Logo = table.Column<byte[]>(nullable: true),
                    Catalog = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brand_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brand_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    Map = table.Column<byte[]>(nullable: true),
                    Flag = table.Column<byte[]>(nullable: true),
                    Population = table.Column<float>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    CallingCode = table.Column<int>(nullable: true),
                    Capital = table.Column<string>(nullable: true),
                    RangeIp = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Province_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SearchFiltersOnCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: true),
                    SearchFiltersId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFiltersOnCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchFiltersOnCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SearchFiltersOnCategory_SearchFilters_SearchFiltersId",
                        column: x => x.SearchFiltersId,
                        principalTable: "SearchFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCategoryVisit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    VisitedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategoryVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCategoryVisit_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCategoryVisit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SellerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    DocumentFile = table.Column<byte[]>(nullable: true),
                    DocumentPath = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerDocuments_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandModified",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrandId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Comment = table.Column<string>(nullable: true),
                    LastBrandBackupJson = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandModified_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrandModified_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    FoundedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturer_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Manufacturer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    Map = table.Column<byte[]>(nullable: true),
                    Flag = table.Column<byte[]>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RangeIp = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    CallingCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAbstract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ManufacturerId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    BaseSize = table.Column<int>(nullable: true),
                    Weight = table.Column<double>(nullable: true),
                    Dimensions = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    BasePrice = table.Column<decimal>(type: "Money", nullable: true),
                    Rank = table.Column<double>(nullable: true),
                    ContentAvailable = table.Column<bool>(nullable: false, defaultValue: false),
                    TolerancQuantity = table.Column<int>(nullable: true),
                    TotalVisit = table.Column<int>(nullable: true),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAbstract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAbstract_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAbstract_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAbstract_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAbstract_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    UserAddress = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ModifyDateTime = table.Column<DateTime>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Logo = table.Column<byte[]>(nullable: true),
                    LogoPath = table.Column<string>(nullable: true),
                    SubDomain = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManufacturerId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturerAddress_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManufacturerAddress_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManufacturerAddress_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManufacturerAddress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    LatinTitle = table.Column<string>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true),
                    Summery = table.Column<string>(nullable: true),
                    MainImage = table.Column<byte[]>(nullable: true),
                    MainImagePath = table.Column<string>(nullable: true),
                    ContentHtml = table.Column<string>(nullable: true),
                    StyleCss = table.Column<string>(nullable: true),
                    RelatedProductId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WrittenDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    CountOfVisit = table.Column<int>(nullable: true),
                    Writer = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    RelatedPhotoGalleryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_PhotoGallery_RelatedPhotoGalleryId",
                        column: x => x.RelatedPhotoGalleryId,
                        principalTable: "PhotoGallery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_ProductAbstract_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralPage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    LatinTitle = table.Column<string>(nullable: true),
                    AliasTitle = table.Column<string>(nullable: true),
                    MetaTagJson = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ContentHtml = table.Column<string>(nullable: true),
                    StyleCss = table.Column<string>(nullable: true),
                    ImageIconPath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegdDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Tags = table.Column<string>(nullable: true),
                    Links = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    RelatedProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralPage_ProductAbstract_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralPage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDescription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SummaryHtml = table.Column<string>(nullable: true),
                    LatinSummaryHtml = table.Column<string>(nullable: true),
                    DescriptionHtml = table.Column<string>(nullable: true),
                    LatinDescriptionHtml = table.Column<string>(nullable: true),
                    DescriptionStyle = table.Column<string>(nullable: true),
                    FeatureHtml = table.Column<string>(nullable: true),
                    FeatureStyle = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ProductAbstractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDescription_ProductAbstract_ProductAbstractId",
                        column: x => x.ProductAbstractId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDescription_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGuarantee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    GuaranteeProviderId = table.Column<int>(nullable: true),
                    GuaranteeType = table.Column<string>(nullable: true),
                    ExtraMoney = table.Column<decimal>(type: "Money", nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Policy = table.Column<string>(nullable: true),
                    Term = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<string>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGuarantee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGuarantee_GuaranteeProvider_GuaranteeProviderId",
                        column: x => x.GuaranteeProviderId,
                        principalTable: "GuaranteeProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGuarantee_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductGuarantee_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    BigImage = table.Column<byte[]>(nullable: true),
                    BigImagePath = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ImageThumbnail = table.Column<byte[]>(nullable: true),
                    ImageThumbnailPath = table.Column<string>(nullable: true),
                    ImageTinyThumbnail = table.Column<byte[]>(nullable: true),
                    ImageTinyThumbnailPath = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    VolumeSize = table.Column<string>(nullable: true),
                    DimensionSize = table.Column<string>(nullable: true),
                    GrayScale = table.Column<bool>(nullable: false, defaultValue: false),
                    Compressed = table.Column<bool>(nullable: false, defaultValue: false),
                    IsMainImage = table.Column<bool>(nullable: false, defaultValue: false),
                    ImageFormat = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductModified",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Comment = table.Column<string>(nullable: true),
                    LastProductBackupJson = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductModified_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductModified_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTag_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopSlider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScreenResulationId = table.Column<int>(nullable: true),
                    Priotity = table.Column<int>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ThumbnailImagePath = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Height = table.Column<double>(nullable: true),
                    Width = table.Column<double>(nullable: true),
                    ByteSize = table.Column<double>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    AltName = table.Column<string>(nullable: true),
                    HasButton = table.Column<bool>(nullable: false, defaultValue: false),
                    ButtonContent = table.Column<string>(nullable: true),
                    ButtonLink = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Summery = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    ConnectedCategoryId = table.Column<int>(nullable: true),
                    ConnectedProductId = table.Column<int>(nullable: true),
                    ConnectedBrandId = table.Column<int>(nullable: true),
                    SetForFuture = table.Column<bool>(nullable: false, defaultValue: false),
                    ShowDateTime = table.Column<DateTime>(nullable: true),
                    ExpireDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopSlider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopSlider_Brand_ConnectedBrandId",
                        column: x => x.ConnectedBrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopSlider_Category_ConnectedCategoryId",
                        column: x => x.ConnectedCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopSlider_ProductAbstract_ConnectedProductId",
                        column: x => x.ConnectedProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopSlider_ScreenResulation_ScreenResulationId",
                        column: x => x.ScreenResulationId,
                        principalTable: "ScreenResulation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopSlider_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserFavoriteProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteProduct_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserFavoriteProduct_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    AnonymousUserEmail = table.Column<string>(nullable: true),
                    AnonymousUserIp = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Fake = table.Column<bool>(nullable: true, defaultValue: false),
                    FakeUserName = table.Column<string>(nullable: true),
                    FakeDateTime = table.Column<DateTime>(nullable: true),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    Liked = table.Column<int>(nullable: true),
                    Disliked = table.Column<int>(nullable: true),
                    OpenComment = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductReview_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductReview_UserProductReview_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UserProductReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductReview_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProductVisit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    VisitDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductVisit_ProductAbstract_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProductVisit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleModified",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Comment = table.Column<string>(nullable: true),
                    LastArticleBackupJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleModified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleModified_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleModified_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserArticleReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    AnonymousUserEmail = table.Column<string>(nullable: true),
                    AnonymousUserIp = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Fake = table.Column<bool>(nullable: true),
                    FakeUserName = table.Column<string>(nullable: true),
                    FakeDateTime = table.Column<DateTime>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    OpenComment = table.Column<bool>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticleReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_UserArticleReview_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UserArticleReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserArticleReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralPageModified",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    GeneralPageId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    LastGeneralPageBackupJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralPageModified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralPageModified_GeneralPage_GeneralPageId",
                        column: x => x.GeneralPageId,
                        principalTable: "GeneralPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralPageModified_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagesList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LatinName = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    GeneralPagesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagesList_GeneralPage_GeneralPagesId",
                        column: x => x.GeneralPagesId,
                        principalTable: "GeneralPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeAndDislikeReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProductReviewId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Reaction = table.Column<int>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeAndDislikeReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeAndDislikeReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikeAndDislikeReview_UserProductReview_UserProductReviewId",
                        column: x => x.UserProductReviewId,
                        principalTable: "UserProductReview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Weight = table.Column<double>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: true),
                    Ratio = table.Column<double>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: true),
                    Distance = table.Column<long>(nullable: true),
                    ShippingMethodId = table.Column<int>(nullable: true),
                    Extended = table.Column<bool>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    ExpireDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPrice_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostPrice_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScreenResulotionId = table.Column<int>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    ShowInPageId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    ThumbnailImagePath = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    SetForFuture = table.Column<bool>(nullable: false, defaultValue: false),
                    ShowDateTime = table.Column<DateTime>(nullable: true),
                    ExpireDateTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Height = table.Column<double>(nullable: true),
                    Width = table.Column<double>(nullable: true),
                    ByteSize = table.Column<double>(nullable: true),
                    AltName = table.Column<string>(nullable: true),
                    HasButton = table.Column<bool>(nullable: false, defaultValue: false),
                    ButtonContent = table.Column<string>(nullable: true),
                    ButtonLink = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Summery = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ConnectedCategoryId = table.Column<int>(nullable: true),
                    ConnectedProductId = table.Column<int>(nullable: true),
                    ConnectedBrandId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CollectionName = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Animated = table.Column<bool>(nullable: false, defaultValue: false),
                    GrayScaleOn = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banner_Brand_ConnectedBrandId",
                        column: x => x.ConnectedBrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_Category_ConnectedCategoryId",
                        column: x => x.ConnectedCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_ProductAbstract_ConnectedProductId",
                        column: x => x.ConnectedProductId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_PagesList_ShowInPageId",
                        column: x => x.ShowInPageId,
                        principalTable: "PagesList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Banner_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdditionalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    LatinTitle = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    UserId = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    ProductFeatureId = table.Column<int>(nullable: false),
                    AdditionalFeaturesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionalFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdditionalFeatures_AdditionalFeatures_AdditionalFeaturesId",
                        column: x => x.AdditionalFeaturesId,
                        principalTable: "AdditionalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAdditionalFeatures_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductAbstractId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    ProductFeatureCode = table.Column<long>(nullable: true),
                    ColorId = table.Column<int>(nullable: true),
                    SizeId = table.Column<int>(nullable: true),
                    TextureId = table.Column<int>(nullable: true),
                    MaterialId = table.Column<int>(nullable: true),
                    TotalCount = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: true),
                    Original = table.Column<bool>(nullable: false, defaultValue: true),
                    ForSale = table.Column<bool>(nullable: false),
                    MinimumForSale = table.Column<int>(nullable: true),
                    MaximumForSale = table.Column<int>(nullable: true),
                    MinForWholeSale = table.Column<int>(nullable: true),
                    ProductCodeNavigationId = table.Column<int>(nullable: true),
                    AdditionalFeaturesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeature_AdditionalFeatures_AdditionalFeaturesId",
                        column: x => x.AdditionalFeaturesId,
                        principalTable: "AdditionalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_ProductAbstract_ProductAbstractId",
                        column: x => x.ProductAbstractId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Texture_TextureId",
                        column: x => x.TextureId,
                        principalTable: "Texture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeature_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CouponIndex",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CouponId = table.Column<int>(nullable: true),
                    AutoClear = table.Column<bool>(nullable: false, defaultValue: false),
                    ProductId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    ExpireDateTime = table.Column<DateTime>(nullable: true),
                    DiscountAmount = table.Column<double>(nullable: true),
                    OldPrice = table.Column<decimal>(type: "Money", nullable: true),
                    NewPrice = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponIndex_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponIndex_ProductFeature_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatureModifed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductFeatureId = table.Column<int>(nullable: true),
                    ModifedByUserId = table.Column<string>(nullable: true),
                    LastRecordBackupJson = table.Column<string>(nullable: true),
                    ModifedDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatureModifed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatureModifed_AspNetUsers_ModifedByUserId",
                        column: x => x.ModifedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFeatureModifed_ProductFeature_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PriceLevelOne = table.Column<decimal>(type: "Money", nullable: true),
                    PriceLevelTwo = table.Column<decimal>(type: "Money", nullable: true),
                    PriceLevelThree = table.Column<decimal>(type: "Money", nullable: true),
                    PriceLevelFour = table.Column<decimal>(type: "Money", nullable: true),
                    PriceLevelFive = table.Column<decimal>(type: "Money", nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    AutoSync = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    BasedOnCurrencyPrice = table.Column<decimal>(type: "Money", nullable: true),
                    BasedOnForeignCurrency = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrice_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrice_ProductFeature_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSold",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductAbstractId = table.Column<int>(nullable: true),
                    ProductFeatureId = table.Column<int>(nullable: true),
                    ProductFeatureCode = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSold_ProductAbstract_ProductAbstractId",
                        column: x => x.ProductAbstractId,
                        principalTable: "ProductAbstract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSold_ProductFeature_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    RegDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    CustomerId = table.Column<string>(nullable: true),
                    TaxIncluded = table.Column<bool>(nullable: false, defaultValue: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: true),
                    ShippingMethodId = table.Column<int>(nullable: true),
                    AdditionalPostPriceId = table.Column<int>(nullable: true),
                    AdditionalPostPriceAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TrackingCode = table.Column<string>(nullable: true),
                    SellerId = table.Column<int>(nullable: true),
                    Approved = table.Column<bool>(nullable: false, defaultValue: false),
                    ApprovedByUserId = table.Column<string>(nullable: true),
                    ApprovedDateTime = table.Column<DateTime>(nullable: true),
                    CouponId = table.Column<int>(nullable: true),
                    CouponIndexId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true),
                    BranchId = table.Column<int>(nullable: true),
                    BankId = table.Column<int>(nullable: true),
                    BankAccountId = table.Column<int>(nullable: true),
                    PaidAtHome = table.Column<bool>(nullable: false, defaultValue: false),
                    IsPaid = table.Column<bool>(nullable: false, defaultValue: false),
                    OnlinePaid = table.Column<bool>(nullable: false, defaultValue: false),
                    RecievedTokenFromBank = table.Column<string>(nullable: true),
                    Sent = table.Column<bool>(nullable: false, defaultValue: false),
                    Delivered = table.Column<bool>(nullable: false, defaultValue: false),
                    GiftAvailable = table.Column<bool>(nullable: false, defaultValue: false),
                    SendAsGift = table.Column<bool>(nullable: false, defaultValue: false),
                    GiftAddress = table.Column<string>(nullable: true),
                    GiftPhoneNumber = table.Column<string>(nullable: true),
                    GiftWrap = table.Column<bool>(nullable: false, defaultValue: false),
                    GiftComment = table.Column<string>(nullable: true),
                    CalculatedPrice = table.Column<decimal>(type: "Money", nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    SendDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_PostPrice_AdditionalPostPriceId",
                        column: x => x.AdditionalPostPriceId,
                        principalTable: "PostPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_AspNetUsers_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_CouponIndex_CouponIndexId",
                        column: x => x.CouponIndexId,
                        principalTable: "CouponIndex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: true),
                    ProductFeatureId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: true),
                    RawPrice = table.Column<decimal>(type: "Money", nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceProduct_ProductFeature_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: false),
                    TrackingCode = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true, defaultValue: true),
                    Accepted = table.Column<bool>(nullable: true, defaultValue: false),
                    TotalPaid = table.Column<decimal>(type: "Money", nullable: false),
                    RegDateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    PaidDateTime = table.Column<DateTime>(nullable: true),
                    TokenKey = table.Column<string>(nullable: true),
                    BankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFeatures_ParentId",
                table: "AdditionalFeatures",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFeatures_UserId",
                table: "AdditionalFeatures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceId",
                table: "Address",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_ParentId",
                table: "AdminMenu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_UserId",
                table: "AdminMenu",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_RelatedPhotoGalleryId",
                table: "Article",
                column: "RelatedPhotoGalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_RelatedProductId",
                table: "Article",
                column: "RelatedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_UserId",
                table: "Article",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleModified_ArticleId",
                table: "ArticleModified",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleModified_UserId",
                table: "ArticleModified",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefinedByUserId",
                table: "AspNetUsers",
                column: "DefinedByUserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankId",
                table: "BankAccount",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ConnectedBrandId",
                table: "Banner",
                column: "ConnectedBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ConnectedCategoryId",
                table: "Banner",
                column: "ConnectedCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ConnectedProductId",
                table: "Banner",
                column: "ConnectedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_ShowInPageId",
                table: "Banner",
                column: "ShowInPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_TypeId",
                table: "Banner",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Banner_UserId",
                table: "Banner",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_ByUserId",
                table: "BlockedUser",
                column: "ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_LoginFailureId",
                table: "BlockedUser",
                column: "LoginFailureId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_UserLogId",
                table: "BlockedUser",
                column: "UserLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CityId",
                table: "Branch",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_SellerId",
                table: "Branch",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_UserId",
                table: "Branch",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_CountryId",
                table: "Brand",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UserId",
                table: "Brand",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandModified_BrandId",
                table: "BrandModified",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandModified_UserId",
                table: "BrandModified",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_FieldId",
                table: "Category",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_ApprovedByUserId",
                table: "ContactUs",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_ApprovedByUserId",
                table: "Coupon",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_SpecialUserId",
                table: "Coupon",
                column: "SpecialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_UserId",
                table: "Coupon",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponIndex_CouponId",
                table: "CouponIndex",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponIndex_ProductId",
                table: "CouponIndex",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_UserId",
                table: "Field",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralPage_RelatedProductId",
                table: "GeneralPage",
                column: "RelatedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralPage_UserId",
                table: "GeneralPage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralPageModified_GeneralPageId",
                table: "GeneralPageModified",
                column: "GeneralPageId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralPageModified_UserId",
                table: "GeneralPageModified",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuaranteeProvider_UserId",
                table: "GuaranteeProvider",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_AdditionalPostPriceId",
                table: "Invoice",
                column: "AdditionalPostPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ApprovedByUserId",
                table: "Invoice",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BankAccountId",
                table: "Invoice",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BankId",
                table: "Invoice",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CouponId",
                table: "Invoice",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CouponIndexId",
                table: "Invoice",
                column: "CouponIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SellerId",
                table: "Invoice",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ShippingMethodId",
                table: "Invoice",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_InvoiceId",
                table: "InvoiceProduct",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProduct_ProductFeatureId",
                table: "InvoiceProduct",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeAndDislikeReview_UserId",
                table: "LikeAndDislikeReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeAndDislikeReview_UserProductReviewId",
                table: "LikeAndDislikeReview",
                column: "UserProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_BrandId",
                table: "Manufacturer",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_UserId",
                table: "Manufacturer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerAddress_CityId",
                table: "ManufacturerAddress",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerAddress_ManufacturerId",
                table: "ManufacturerAddress",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerAddress_ProvinceId",
                table: "ManufacturerAddress",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerAddress_UserId",
                table: "ManufacturerAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesList_GeneralPagesId",
                table: "PagesList",
                column: "GeneralPagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BankId",
                table: "Payments",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGallery_SubjectId",
                table: "PhotoGallery",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoGallery_UserId",
                table: "PhotoGallery",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPrice_ShippingMethodId",
                table: "PostPrice",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPrice_WarehouseId",
                table: "PostPrice",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAbstract_BrandId",
                table: "ProductAbstract",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAbstract_CategoryId",
                table: "ProductAbstract",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAbstract_ManufacturerId",
                table: "ProductAbstract",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAbstract_UserId",
                table: "ProductAbstract",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalFeatures_AdditionalFeaturesId",
                table: "ProductAdditionalFeatures",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalFeatures_ProductFeatureId",
                table: "ProductAdditionalFeatures",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalFeatures_UserId",
                table: "ProductAdditionalFeatures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescription_ProductAbstractId",
                table: "ProductDescription",
                column: "ProductAbstractId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescription_UserId",
                table: "ProductDescription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_AdditionalFeaturesId",
                table: "ProductFeature",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ColorId",
                table: "ProductFeature",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_MaterialId",
                table: "ProductFeature",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductAbstractId",
                table: "ProductFeature",
                column: "ProductAbstractId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductCodeNavigationId",
                table: "ProductFeature",
                column: "ProductCodeNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_SellerId",
                table: "ProductFeature",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_SizeId",
                table: "ProductFeature",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_TextureId",
                table: "ProductFeature",
                column: "TextureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_UserId",
                table: "ProductFeature",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_WarehouseId",
                table: "ProductFeature",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureModifed_ModifedByUserId",
                table: "ProductFeatureModifed",
                column: "ModifedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureModifed_ProductFeatureId",
                table: "ProductFeatureModifed",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGuarantee_GuaranteeProviderId",
                table: "ProductGuarantee",
                column: "GuaranteeProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGuarantee_ProductId",
                table: "ProductGuarantee",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGuarantee_UserId",
                table: "ProductGuarantee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_UserId",
                table: "ProductImage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModified_ProductId",
                table: "ProductModified",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductModified_UserId",
                table: "ProductModified",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_CurrencyId",
                table: "ProductPrice",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_ProductId",
                table: "ProductPrice",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSold_ProductAbstractId",
                table: "ProductSold",
                column: "ProductAbstractId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSold_ProductFeatureId",
                table: "ProductSold",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_ProductId",
                table: "ProductTag",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagId",
                table: "ProductTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CountryId",
                table: "Province",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchedItems_UserLogId",
                table: "SearchedItems",
                column: "UserLogId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFilters_UserId",
                table: "SearchFilters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFiltersOnCategory_CategoryId",
                table: "SearchFiltersOnCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFiltersOnCategory_SearchFiltersId",
                table: "SearchFiltersOnCategory",
                column: "SearchFiltersId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_BankAccountId",
                table: "Seller",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_UserId",
                table: "Seller",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerDocuments_SellerId",
                table: "SellerDocuments",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_UserId",
                table: "Setting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethod_UserId",
                table: "ShippingMethod",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteGeneralInfo_UserId",
                table: "SiteGeneralInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UserId",
                table: "Subject",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CouponId",
                table: "Tag",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserId",
                table: "Tag",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopSlider_ConnectedBrandId",
                table: "TopSlider",
                column: "ConnectedBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TopSlider_ConnectedCategoryId",
                table: "TopSlider",
                column: "ConnectedCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TopSlider_ConnectedProductId",
                table: "TopSlider",
                column: "ConnectedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TopSlider_ScreenResulationId",
                table: "TopSlider",
                column: "ScreenResulationId");

            migrationBuilder.CreateIndex(
                name: "IX_TopSlider_UserId",
                table: "TopSlider",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ApprovedByUserId",
                table: "UserArticleReview",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ArticleId",
                table: "UserArticleReview",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_ParentId",
                table: "UserArticleReview",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticleReview_UserId",
                table: "UserArticleReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBehaviorTracking_UserLogId",
                table: "UserBehaviorTracking",
                column: "UserLogId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryVisit_CategoryId",
                table: "UserCategoryVisit",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategoryVisit_UserId",
                table: "UserCategoryVisit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteProduct_ProductId",
                table: "UserFavoriteProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteProduct_UserId",
                table: "UserFavoriteProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImage_UserId",
                table: "UserImage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_UserId",
                table: "UserLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModified_ModifedByUserId",
                table: "UserModified",
                column: "ModifedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModified_UserId",
                table: "UserModified",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductReview_ApprovedByUserId",
                table: "UserProductReview",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductReview_ParentId",
                table: "UserProductReview",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductReview_ProductId",
                table: "UserProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductReview_UserId",
                table: "UserProductReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductVisit_ProductId",
                table: "UserProductVisit",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductVisit_UserId",
                table: "UserProductVisit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReported_ReportedByUserId",
                table: "UserReported",
                column: "ReportedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReported_UserId",
                table: "UserReported",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCode_UserId",
                table: "VerificationCode",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouse",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalFeatures_ProductFeature_ProductFeatureId",
                table: "ProductAdditionalFeatures",
                column: "ProductFeatureId",
                principalTable: "ProductFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeature_ProductSold_ProductCodeNavigationId",
                table: "ProductFeature",
                column: "ProductCodeNavigationId",
                principalTable: "ProductSold",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalFeatures_AspNetUsers_UserId",
                table: "AdditionalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_AspNetUsers_UserId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Brand_AspNetUsers_UserId",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_UserId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Field_AspNetUsers_UserId",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturer_AspNetUsers_UserId",
                table: "Manufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAbstract_AspNetUsers_UserId",
                table: "ProductAbstract");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_AspNetUsers_UserId",
                table: "ProductFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_AspNetUsers_UserId",
                table: "Seller");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_City_CityId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_ProductAbstract_ProductAbstractId",
                table: "ProductFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSold_ProductAbstract_ProductAbstractId",
                table: "ProductSold");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Bank_BankId",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Seller_SellerId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_Seller_SellerId",
                table: "ProductFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSold_ProductFeature_ProductFeatureId",
                table: "ProductSold");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AdminMenu");

            migrationBuilder.DropTable(
                name: "ArticleModified");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "BlockedUser");

            migrationBuilder.DropTable(
                name: "BrandModified");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "GeneralPageModified");

            migrationBuilder.DropTable(
                name: "InvoiceProduct");

            migrationBuilder.DropTable(
                name: "LikeAndDislikeReview");

            migrationBuilder.DropTable(
                name: "ManufacturerAddress");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductAdditionalFeatures");

            migrationBuilder.DropTable(
                name: "ProductDescription");

            migrationBuilder.DropTable(
                name: "ProductFeatureModifed");

            migrationBuilder.DropTable(
                name: "ProductGuarantee");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductModified");

            migrationBuilder.DropTable(
                name: "ProductPrice");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "SearchedItems");

            migrationBuilder.DropTable(
                name: "SearchFiltersOnCategory");

            migrationBuilder.DropTable(
                name: "SellerDocuments");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "SiteGeneralInfo");

            migrationBuilder.DropTable(
                name: "TopSlider");

            migrationBuilder.DropTable(
                name: "UserArticleReview");

            migrationBuilder.DropTable(
                name: "UserBehaviorTracking");

            migrationBuilder.DropTable(
                name: "UserCategoryVisit");

            migrationBuilder.DropTable(
                name: "UserFavoriteProduct");

            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DropTable(
                name: "UserModified");

            migrationBuilder.DropTable(
                name: "UserProductVisit");

            migrationBuilder.DropTable(
                name: "UserReported");

            migrationBuilder.DropTable(
                name: "VerificationCode");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PagesList");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "LoginFailure");

            migrationBuilder.DropTable(
                name: "UserProductReview");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "GuaranteeProvider");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "SearchFilters");

            migrationBuilder.DropTable(
                name: "ScreenResulation");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "GeneralPage");

            migrationBuilder.DropTable(
                name: "PostPrice");

            migrationBuilder.DropTable(
                name: "CouponIndex");

            migrationBuilder.DropTable(
                name: "PhotoGallery");

            migrationBuilder.DropTable(
                name: "ShippingMethod");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "ProductAbstract");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "ProductFeature");

            migrationBuilder.DropTable(
                name: "AdditionalFeatures");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "ProductSold");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Texture");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Branch");
        }
    }
}
