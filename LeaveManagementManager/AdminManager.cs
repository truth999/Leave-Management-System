using System;
using System.Collections.Generic;
using System.Text;
using LeaveManagementModels;
using LeaveManagementRepository;

namespace LeaveManagementManager
{
    public class AdminManager
    {
        public List<AdminLeaveModel> GetAdminLeaveDetails()
        {
            AdminRepository adminRepository = new AdminRepository();
            return adminRepository.GetAdminLeaveDetails();
        }

        public List<EmployeeModel> GetEmployeeDetails()
        {
            AdminRepository adminRepository = new AdminRepository();
            return adminRepository.GetEmployeeDetails();
        }

        public List<AdminLeaveModel> GetHistoryDetails()
        {
            AdminRepository adminRepository = new AdminRepository();
            return adminRepository.GetHistoryDetails();
        }

        public void ApproveLeave(AdminLeaveModel adminLeave)
        {
            AdminRepository adminRepository = new AdminRepository();
            adminRepository.ApproveLeave(adminLeave);
        }

        public void RejectLeave(AdminLeaveModel adminLeave)
        {
            AdminRepository adminRepository = new AdminRepository();
            adminRepository.RejectLeave(adminLeave);
        }

        public void BlockUser(int empid)
        {
            AdminRepository adminRepository = new AdminRepository();
            adminRepository.BlockUser(empid);
        }

        public void UnblockUser(int empid)
        {
            AdminRepository adminRepository = new AdminRepository();
            adminRepository.UnblockUser(empid);
        }
    }
}
