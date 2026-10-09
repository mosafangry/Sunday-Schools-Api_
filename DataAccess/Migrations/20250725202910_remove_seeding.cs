using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SubscriptionPermissions",
                keyColumn: "SubscriptionPermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Category", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "File", "Upload files", true, "FileUpload" },
                    { 2, "File", "Download files", true, "FileDownload" },
                    { 3, "Report", "View reports", true, "ViewReports" },
                    { 4, "Report", "Create reports", true, "CreateReports" },
                    { 5, "Admin", "Manage users", true, "UserManagement" },
                    { 6, "Admin", "Manage system settings", true, "SystemSettings" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator with full access", true, "Admin" },
                    { 2, "Regular user", true, "User" },
                    { 3, "Premium user with extended access", true, "Premium" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionId", "Description", "DurationInDays", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Basic plan", 30, true, "Basic", 9.99m },
                    { 2, "Premium plan", 30, true, "Premium", 19.99m },
                    { 3, "Enterprise plan", 30, true, "Enterprise", 49.99m }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RolePermissionId", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 3, 2 },
                    { 8, 1, 3 },
                    { 9, 2, 3 },
                    { 10, 3, 3 },
                    { 11, 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPermissions",
                columns: new[] { "SubscriptionPermissionId", "PermissionId", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 3, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 2 },
                    { 4, 3, 2 },
                    { 5, 4, 2 },
                    { 6, 1, 3 },
                    { 7, 2, 3 },
                    { 8, 3, 3 },
                    { 9, 4, 3 },
                    { 10, 5, 3 }
                });
        }
    }
}
