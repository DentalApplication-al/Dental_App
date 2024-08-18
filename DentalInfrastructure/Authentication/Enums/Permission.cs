namespace DentalInfrastructure.Authentication.Enums
{
    public enum Permission
    {
        ADMIN = 2,

        // Staff permissions
        ADDSTAFF = 3,
        GETALLSTAFF = 4,
        GETSTAFFBYID = 5,
        DELETESTAF = 6,
        UPDATESTAFF = 7,

        //ClientPermissions
        ADDCLIENT = 8,
        GETALLCLIENTS = 9,
        GETCLIENTBYID = 10,
        UPDATECLIENT = 11,
        DELETECLIENT = 12,

        // Service Permissions
        ADDSERVICE = 13,
        GETALLSERVICES = 14,
        GETSERVICEBYID = 15,
        UPDATESRVICE = 16,
        DELETESERVICE = 17,

        // Appointment permissions
        ADDAPPOINTMENT = 18,
        UPDATEAPPOINTMENT = 19,
        DELETEAPPOINTMENT = 20,
        GETALLAPPOINTMENTS = 21,
        GETAPPOINTMENTBYID = 22,

        // Payment permissions
        ADDPAYMENT = 23,
        GETALLPAYMENTS = 24,
        GETPAYMENTBYID = 25,
        UPDATEPAYMENT = 26,
        DELETEPAYMENT = 27,
    }
}
