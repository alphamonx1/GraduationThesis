using CAPSTONEPROJECT.DataModels.EmpDataModel;
using CAPSTONEPROJECT.Models;

using ExcelDataReader;

using Microsoft.AspNetCore.Http;

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class EmployeeService
    {
        private readonly LugContext _context;

        public EmployeeService(LugContext context)
        {
            _context = context;
        }

        public List<EmpResponse> GetAll()
        {
            var query = _context.Employees
                .Select(emp => new EmpResponse
                {
                    EmployeeID = emp.EmployeeId,
                    ProfileImage = emp.Image,
                    FullName = emp.FullName,
                    Gender = emp.Gender,
                    Ethnic = emp.Ethnic,
                    DateOfBirth = emp.DateOfBirth,
                    PlaceOfBirth = emp.PlaceOfBirth,
                    PhoneNumber = emp.PhoneNumber,
                    Email = emp.Email,
                    MaritalStatus = emp.MaritalStatus,
                    Religion = emp.Religion,
                    PermanentAddress = emp.PermanentAddress,
                    TemporaryAddress = emp.TemporaryAddress,
                    Workplace = _context.Workplaces.Where(x => x.WorkplaceId == emp.WorkplaceId).Select(x => x.WorkplaceName).Single(),
                    EmployeeTypeName = _context.EmployeeTypes.Where(x => x.EmployeeTypeId == emp.EmployeeTypeId).Select(x => x.EmployeeTypeName).Single(),
                    Position = _context.Positions.Where(x => x.PositionId == emp.PositionId).Select(x => x.PositionName).Single(),
                    IDCardDateOfIssue = emp.IdcardDateOfIssue,
                    IDCardPlaceOfIssue = emp.IdcardIssueBy,
                    DelFlag = emp.DelFlag,


                }
                );

            var result = new List<EmpResponse>();
            foreach (var item in query)
            {
                result.Add(item);
            }

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }



        }

        public EmpResponse GetByID(string id)
        {
            var query = _context.Employees.Where(x => x.EmployeeId == id && x.DelFlag == false)
                .Select(emp => new EmpResponse
                {
                    EmployeeID = emp.EmployeeId,
                    ProfileImage = emp.Image,
                    FullName = emp.FullName,
                    Gender = emp.Gender,
                    Ethnic = emp.Ethnic,
                    DateOfBirth = emp.DateOfBirth,
                    PlaceOfBirth = emp.PlaceOfBirth,
                    PhoneNumber = emp.PhoneNumber,
                    Email = emp.Email,
                    MaritalStatus = emp.MaritalStatus,
                    Religion = emp.Religion,
                    PermanentAddress = emp.PermanentAddress,
                    TemporaryAddress = emp.TemporaryAddress,
                    Workplace = _context.Workplaces.Where(x => x.WorkplaceId == emp.WorkplaceId).Select(x => x.WorkplaceName).Single(),
                    EmployeeTypeName = _context.EmployeeTypes.Where(x => x.EmployeeTypeId == emp.EmployeeTypeId).Select(x => x.EmployeeTypeName).Single(),
                    Position = _context.Positions.Where(x => x.PositionId == emp.PositionId).Select(x => x.PositionName).Single(),
                    IDCardDateOfIssue = emp.IdcardDateOfIssue,
                    IDCardPlaceOfIssue = emp.IdcardIssueBy,
                    DelFlag = emp.DelFlag,

                }).FirstOrDefault();

            return query;
        }

        public List<EmpResponse> GetByWorkplace(string WorkplaceID)
        {
            var query = _context.Employees.Where(x => x.WorkplaceId == WorkplaceID)
                .Select(emp => new EmpResponse
                {
                    EmployeeID = emp.EmployeeId,
                    ProfileImage = emp.Image,
                    FullName = emp.FullName,
                    Gender = emp.Gender,
                    Ethnic = emp.Ethnic,
                    DateOfBirth = emp.DateOfBirth,
                    PlaceOfBirth = emp.PlaceOfBirth,
                    PhoneNumber = emp.PhoneNumber,
                    Email = emp.Email,
                    MaritalStatus = emp.MaritalStatus,
                    Religion = emp.Religion,
                    PermanentAddress = emp.PermanentAddress,
                    TemporaryAddress = emp.TemporaryAddress,
                    Workplace = _context.Workplaces.Where(x => x.WorkplaceId == emp.WorkplaceId).Select(x => x.WorkplaceName).Single(),
                    EmployeeTypeName = _context.EmployeeTypes.Where(x => x.EmployeeTypeId == emp.EmployeeTypeId).Select(x => x.EmployeeTypeName).Single(),
                    Position = _context.Positions.Where(x => x.PositionId == emp.PositionId).Select(x => x.PositionName).Single(),
                    IDCardDateOfIssue = emp.IdcardDateOfIssue,
                    IDCardPlaceOfIssue = emp.IdcardIssueBy,
                    DelFlag = emp.DelFlag,

                }
                );

            var result = new List<EmpResponse>();
            foreach (var item in query)
            {
                result.Add(item);
            }

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }


        }

        public int CountByWorkplace(string WorkplaceID)
        {
            var result = _context.Employees.Count(x => x.WorkplaceId == WorkplaceID);
            return result;
        }

        //public bool ImportEmpByExcel(IFormFile file)
        //{
        //    bool status = false;
        //    DataSet dsexcelRecords = new DataSet();
        //    IExcelDataReader reader = null;
        //    IFormFile Inputfile = null;
        //    Stream FileStream = null;

        //    if (Inputfile != null)
        //    {
        //        Inputfile = file;
        //        FileStream = Inputfile.OpenReadStream();

        //        if (Inputfile != null && FileStream != null)
        //        {
        //            if (Inputfile.FileName.EndsWith(".xls"))
        //            {
        //                reader = ExcelReaderFactory.CreateBinaryReader(FileStream);

        //            }
        //            else if (Inputfile.FileName.EndsWith(".xlsx"))
        //            {
        //                reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
        //            }
        //            else
        //            {
        //                status = false;
        //            }

        //            dsexcelRecords = reader.AsDataSet();
        //            reader.Close();
        //            if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
        //            {
        //                DataTable dtStudentRecords = dsexcelRecords.Tables[0];
        //                for (int i = 0; i < dtStudentRecords.Rows.Count; i++)
        //                {
        //                    Employee emp = new()
        //                    {
        //                        EmployeeId = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Image = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        FullName = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Gender = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Ethnic = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        DateOfBirth = Convert.ToDateTime(dtStudentRecords.Rows[i]),
        //                        PlaceOfBirth = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        PhoneNumber = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Email = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        MaritalStatus = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Religion = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        PermanentAddress = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        TemporaryAddress = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        WorkplaceId = Convert.ToString(_context.Workplaces.Where(x => x.WorkplaceName == Convert.ToString(dtStudentRecords.Rows[i])).Select(x => x.WorkplaceId)),
        //                        EmployeeTypeId = Convert.ToInt32(dtStudentRecords.Rows[i]),
        //                        TotalOffDay = Convert.ToInt32(dtStudentRecords.Rows[i]),
        //                        PositionId = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        IdcardDateOfIssue = Convert.ToDateTime(dtStudentRecords.Rows[i]),
        //                        IdcardIssueBy = Convert.ToString(dtStudentRecords.Rows[i]),
        //                        Contact = Convert.ToString(dtStudentRecords.Rows[i]),

        //                    };

        //                    _context.Employees.Add(emp);
        //                    status = _context.SaveChanges() > 0;
        //                }
        //            }
        //            else
        //            {
        //                status = false;
        //            }
        //        }
        //        else
        //        {
        //            status = false;
        //        }

        //    }
        //    return status;
        //}

        public bool CreateEmp(EmpCreateModel dataModel)
        {
            bool flag = false;
            int EmployeeTypeID;
            if (dataModel.PositionID == "S01" || dataModel.PositionID == "SPT")
            {
                 EmployeeTypeID = 2;
            }
            else
            {
                EmployeeTypeID = 1;
            }

            try
            {
                var emp = new Employee
                {
                    EmployeeId = dataModel.EmployeeID,
                    Image = dataModel.ProfileImage,
                    FullName = dataModel.FullName,
                    Gender = dataModel.Gender,
                    Ethnic = dataModel.Ethnic,
                    DateOfBirth = dataModel.DateOfBirth,
                    PlaceOfBirth = dataModel.PlaceOfBirth,
                    PhoneNumber = dataModel.PhoneNumber,
                    Email = dataModel.Email,
                    MaritalStatus = dataModel.MaritalStatus,
                    Religion = dataModel.Religion,
                    PermanentAddress = dataModel.PermanentAddress,
                    TemporaryAddress = dataModel.TemporaryAddress,
                    WorkplaceId = dataModel.WorkplaceID,
                    EmployeeTypeId = EmployeeTypeID,
                    PositionId = dataModel.PositionID,
                    IdcardDateOfIssue = dataModel.IDCardDateOfIssue,
                    IdcardIssueBy = dataModel.IDCardPlaceOfIssue,
                    TotalOffDay = 12,
                };
                if (EmployeeExist(emp.EmployeeId))
                {
                    flag = false;
                }
                else
                {
                    _context.Add<Employee>(emp);
                    flag = _context.SaveChanges() > 0;
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return flag;
        }

        public bool UpdateEmp(string id, EmpUpdateModel dataModel)
        {
            bool status = false;
            int EmployeeTypeID;
            if (dataModel.PositionID == "S01" || dataModel.PositionID == "SPT")
            {
                EmployeeTypeID = 2;
            }
            else
            {
                EmployeeTypeID = 1;
            }

            try
            {
                var emp = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
                emp.Image = dataModel.ProfileImage;
                emp.FullName = dataModel.FullName;
                emp.Gender = dataModel.Gender;
                emp.Ethnic = dataModel.Ethnic;
                emp.DateOfBirth = dataModel.DateOfBirth;
                emp.PlaceOfBirth = dataModel.PlaceOfBirth;
                emp.PhoneNumber = dataModel.PhoneNumber;
                emp.Email = dataModel.Email;
                emp.MaritalStatus = dataModel.MaritalStatus;
                emp.Religion = dataModel.Religion;
                emp.PermanentAddress = dataModel.PermanentAddress;
                emp.TemporaryAddress = dataModel.TemporaryAddress;
                emp.WorkplaceId = dataModel.WorkplaceID;
                emp.EmployeeTypeId = EmployeeTypeID;
                emp.PositionId = dataModel.PositionID;
                emp.IdcardDateOfIssue = dataModel.IDCardDateOfIssue;
                emp.IdcardIssueBy = dataModel.IDCardPlaceOfIssue;

                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return status;
        }

        public bool ChangeEmpStatus(string id)
        {
            bool status = false;
            var emp = _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (emp.DelFlag == true)
            {
                emp.DelFlag = false;
            }
            else
            {
                emp.DelFlag = true;
                var account = _context.Accounts.Where(x => x.AccountId == emp.EmployeeId).FirstOrDefault();
                if (account != null)
                {
                    _context.Accounts.Remove(account);
                }

            }


            status = _context.SaveChanges() > 0;

            return status;
        }

        public bool EmployeeExist(string id)
        {
            return _context.Employees.Any(x => x.EmployeeId == id);
        }


    }
}
