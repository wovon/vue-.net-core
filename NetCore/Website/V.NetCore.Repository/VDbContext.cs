using System;
using Microsoft.EntityFrameworkCore;
using V.NetCore.Repository.Domain;
using V.NetCore.Repository.Domain.WorkOrderManagement;

namespace V.NetCore.Repository
{
    public class VDbContext : DbContext
    {
        public VDbContext(DbContextOptions<VDbContext> options)
            : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserDepartment> UserDepartment { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Button> Buttons { get; set; }
        public virtual DbSet<MenuButton> MenuButton { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<RoleMenuButton> RoleMenuButton { get; set; }

        public virtual DbSet<DepartmentRole> DepartmentRole { get; set; }

        public virtual DbSet<OutBox> OutBox { get; set; }
        /// <summary>
        /// 流程
        /// </summary>
        public virtual DbSet<WorkFlow> WorkFlows { get; set; }

        //public virtual DbSet<DepartmentParent> DepartmentParent { get; set; }
        /// <summary>
        /// 选项列表
        /// </summary>
        public virtual DbSet<SelectOption> SelectOptions { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public virtual DbSet<Modifier> Modifiers { get; set; }
        /// <summary>
        /// 问题反馈
        /// </summary>
        public virtual DbSet<WorkOrderManagement> WorkOrderManagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //数据库关系
            modelBuilder.Entity<UserDepartment>()
                .HasKey(x => new { x.UserId, x.DepartmentId });
            modelBuilder.Entity<UserDepartment>().HasOne(x => x.Department).WithMany(x => x.UserDepartments)
                .HasForeignKey(x => x.DepartmentId);
            modelBuilder.Entity<UserDepartment>().HasOne(x => x.User).WithMany(x => x.UserDepartments)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<MenuButton>().HasKey(x => new { x.MenuId,x.ButtonId});
            modelBuilder.Entity<MenuButton>().HasOne(x => x.Menu).WithMany(x => x.MenuButtons)
                .HasForeignKey(x => x.MenuId);
            modelBuilder.Entity<MenuButton>().HasOne(x => x.Button).WithMany(x => x.MenuButtons)
                .HasForeignKey(x => x.ButtonId);





            //modelBuilder.Entity<RoleMenu>().HasKey(x => new {x.MenuId, x.RoleId});
            //modelBuilder.Entity<RoleMenu>().HasOne(x => x.Menu).WithMany(x => x.RoeMenus)
            //    .HasForeignKey(x=>x.MenuId);
            //modelBuilder.Entity<RoleMenu>().HasOne(x => x.Role).WithMany(x => x.RoleMenus)
            //    .HasForeignKey(x => x.RoleId);


            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<DepartmentRole>()
                .HasKey(x => new {x.DepartmentId, x.RoleId});
            modelBuilder.Entity<DepartmentRole>().HasOne(x => x.Department).WithMany(x => x.DepartmentRoles)
                .HasForeignKey(x => x.DepartmentId);
            modelBuilder.Entity<DepartmentRole>().HasOne(x => x.Role).WithMany(x => x.DepartmentRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<ProjectWorkflow>()
                .HasKey(x => new { x.ProjectId, x.WorkflowId });
            modelBuilder.Entity<ProjectWorkflow>().HasOne(x => x.Project).WithMany(x => x.ProjectWorkflow)
                .HasForeignKey(x => x.ProjectId);
            modelBuilder.Entity<ProjectWorkflow>().HasOne(x => x.WorkFlows).WithMany(x => x.ProjectWorkflow)
                .HasForeignKey(x => x.WorkflowId);
            ////种子数据
            //modelBuilder.Entity<User>().HasData(
            //    new User()
            //    {
            //        Id = "7AE40B8E-7C96-4F58-9AB8-002A8E6CD4C4",
            //        Account = "v",
            //        Password = "202CB962AC59075B964B07152D234B",
            //        Name = "v",
            //        Sex = 0,
            //        Status = 0,
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now
            //    }, new User()
            //    {
            //        Id = "D62E2390-E47F-4619-94DF-69D4717F5991",
            //        Account = "a",
            //        Password = "202CB962AC59075B964B07152D234B",
            //        Name = "a",
            //        Sex = 0,
            //        Status = 0,
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now
            //    }, new User()
            //    {
            //        Id = "961C6593-CB5D-4025-9FB0-490D59AEDC0A",
            //        Account = "b",
            //        Password = "202CB962AC59075B964B07152D234B",
            //        Name = "b",
            //        Sex = 0,
            //        Status = 0,
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now
            //    }, new User()
            //    {
            //        Id = "CDF5151A-F14A-4BC9-ADDC-33721D01C23F",
            //        Account = "c",
            //        Password = "202CB962AC59075B964B07152D234B",
            //        Name = "c",
            //        Sex = 0,
            //        Status = 0,
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now
            //    });


            //modelBuilder.Entity<Department>().HasData(
            //    new Department()
            //    {
            //        Id = "F6DBBF8C-13F7-46BB-BE60-91A46C8E29AB",
            //        Name = "第一级",
            //        ParentId = null,
            //        ParentName = "",
            //        Explain = "",
            //        CascadeId = ".0.1.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    }, new Department()
            //    {
            //        Id = "442871DC-9802-4F5F-81C5-A1206CE9DD28",
            //        Name = "第二级",
            //        ParentId = null,
            //        ParentName = "",
            //        Explain = "",
            //        CascadeId = ".0.2.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    }, new Department()
            //    {
            //        Id = "C1AF93CE-802D-4B54-A611-A14BBA51D4A3",
            //        Name = "第三级",
            //        ParentId = null,
            //        ParentName = "",
            //        Explain = "",
            //        CascadeId = ".0.3.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    }, new Department()
            //    {
            //        Id = "6DA535FA-FAF3-4B5B-A143-177F94895118",
            //        Name = "第一级(子集一)",
            //        ParentId = "F6DBBF8C-13F7-46BB-BE60-91A46C8E29AB",
            //        ParentName = "第一级",
            //        Explain = "",
            //        CascadeId = ".0.1.1.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    }, new Department()
            //    {
            //        Id = "D643834D-6C02-455A-8277-33B090D00353",
            //        Name = "第一级",
            //        ParentId = "6DA535FA-FAF3-4B5B-A143-177F94895118",
            //        ParentName = "第一级(子集一)",
            //        Explain = "",
            //        CascadeId = ".0.1.1.1.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    }, new Department()
            //    {
            //        Id = "CF0E5BBE-18B9-4F8E-8F36-EAA30E7CFF44",
            //        Name = "第二级(子集一)",
            //        ParentId = "442871DC-9802-4F5F-81C5-A1206CE9DD28",
            //        ParentName = "第二级",
            //        Explain = "",
            //        CascadeId = ".0.2.1.",
            //        CreateTime = DateTime.Now,
            //        UpdateTime = DateTime.Now,
            //        IsAble = false,
            //        Sort = 1000
            //    });
            //modelBuilder.Entity<UserDepartment>().HasData(
            //    new UserDepartment()
            //    {
            //        UserId = "7AE40B8E-7C96-4F58-9AB8-002A8E6CD4C4",
            //        DepartmentId = "F6DBBF8C-13F7-46BB-BE60-91A46C8E29AB"
            //    }, new UserDepartment()
            //    {
            //        UserId = "7AE40B8E-7C96-4F58-9AB8-002A8E6CD4C4",
            //        DepartmentId = "6DA535FA-FAF3-4B5B-A143-177F94895118"
            //    }, new UserDepartment()
            //    {
            //        UserId = "7AE40B8E-7C96-4F58-9AB8-002A8E6CD4C4",
            //        DepartmentId = "D643834D-6C02-455A-8277-33B090D00353"
            //    }, new UserDepartment()
            //    {
            //        UserId = "D62E2390-E47F-4619-94DF-69D4717F5991",
            //        DepartmentId = "442871DC-9802-4F5F-81C5-A1206CE9DD28"
            //    }, new UserDepartment()
            //    {
            //        UserId = "D62E2390-E47F-4619-94DF-69D4717F5991",
            //        DepartmentId = "CF0E5BBE-18B9-4F8E-8F36-EAA30E7CFF44"
            //    }, new UserDepartment()
            //    {
            //        UserId = "961C6593-CB5D-4025-9FB0-490D59AEDC0A",
            //        DepartmentId = "C1AF93CE-802D-4B54-A611-A14BBA51D4A3"
            //    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
