using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { 6, "Admin", "Manage system settings", true, "SystemSettings" },
                    { 7, "Report", "Update reports", true, "UpdateReports" },
                    { 8, "Report", "Delete reports", true, "DeleteReports" },
                    { 9, "Drug", "View drugs", true, "ViewDrugs" },
                    { 10, "Drug", "Create drugs", true, "CreateDrugs" },
                    { 11, "Drug", "Update drugs", true, "UpdateDrugs" },
                    { 12, "Drug", "Delete drugs", true, "DeleteDrugs" },
                    { 13, "DrugReport", "View drug reports", true, "ViewDrugReports" },
                    { 14, "DrugReport", "Create drug reports", true, "CreateDrugReports" },
                    { 15, "DrugReport", "Update drug reports", true, "UpdateDrugReports" },
                    { 16, "DrugReport", "Delete drug reports", true, "DeleteDrugReports" },
                    { 17, "LiteratureReview", "View literature reviews", true, "ViewLiteratureReviews" },
                    { 18, "LiteratureReview", "Create literature reviews", true, "CreateLiteratureReviews" },
                    { 19, "LiteratureReview", "Update literature reviews", true, "UpdateLiteratureReviews" },
                    { 20, "LiteratureReview", "Delete literature reviews", true, "DeleteLiteratureReviews" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator with full access", true, "Admin" },
                    { 2, "Regular user", true, "User" },
                    { 3, "Premium user with extended access", true, "Premium" },
                    { 4, "Company representative with access to reports, drugs, drug reports, and literature reviews", true, "CompanyRepresentative" }
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
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 },
                    { 12, 12, 1 },
                    { 13, 13, 1 },
                    { 14, 14, 1 },
                    { 15, 15, 1 },
                    { 16, 16, 1 },
                    { 17, 17, 1 },
                    { 18, 18, 1 },
                    { 19, 19, 1 },
                    { 20, 20, 1 },
                    { 21, 3, 2 },
                    { 22, 1, 3 },
                    { 23, 2, 3 },
                    { 24, 3, 3 },
                    { 25, 4, 3 },
                    { 26, 3, 4 },
                    { 27, 4, 4 },
                    { 28, 7, 4 },
                    { 29, 8, 4 },
                    { 30, 9, 4 },
                    { 31, 10, 4 },
                    { 32, 11, 4 },
                    { 33, 12, 4 },
                    { 34, 13, 4 },
                    { 35, 14, 4 },
                    { 36, 15, 4 },
                    { 37, 16, 4 },
                    { 38, 17, 4 },
                    { 39, 18, 4 },
                    { 40, 19, 4 },
                    { 41, 20, 4 }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RolePermissionId",
                keyValue: 41);

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
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 20);

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
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4);

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
    }
}
