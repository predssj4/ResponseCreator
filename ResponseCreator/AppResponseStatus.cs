namespace ResponseCreator
{
    public enum AppResponseStatus
    {
        Success = 1,

        // expected errors, may occur
        Errors = 2,

        // unexpected, unhandled errors
        Fatal = 3,
    }
}
