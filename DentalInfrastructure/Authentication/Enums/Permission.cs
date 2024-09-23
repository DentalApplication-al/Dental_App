namespace DentalInfrastructure.Authentication.Enums
{
    public enum Permission
    {
        ADMIN = 2,

        // Staff permissions
        ADDSTAFF,
        GETALLSTAFF,
        GETSTAFFBYID,
        GETDOCTORS,
        UPDATESTAFF,
        DELETESTAF,
        STAFF_CHANGE_STATUS,

        //ClientPermissions
        CLIENT_ADD,
        CLIENT_GET_ALL,
        CLIENT_GET_BY_ID,
        CLIENT_GET_APPOINTMENTS,
        CLIENT_UPDATE,
        CLIENT_DELETE,
        CLIENT_UPLOAD_FILE,
        CLIENT_DELETE_FILE,

        // Service Permissions
        ADDSERVICE,
        GETALLSERVICES,
        GETSERVICEBYID,
        UPDATESRVICE,
        DELETESERVICE,

        // Appointment permissions
        ADDAPPOINTMENT,
        UPDATEAPPOINTMENT,
        DELETEAPPOINTMENT,
        GETALLAPPOINTMENTS,
        GETAPPOINTMENTBYID,

        // Payment permissions
        ADDPAYMENT,
        GETALLPAYMENTS,
        GETPAYMENTBYID,
        UPDATEPAYMENT,
        DELETEPAYMENT,
    }
}
