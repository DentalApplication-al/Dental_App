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

        //ClientPermissions
        ADDCLIENT,
        GETALLCLIENTS,
        GETCLIENTBYID,
        UPDATECLIENT,
        DELETECLIENT,

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
