namespace FMAPI.Helpers
{
    public class MessageHelper
    {
        public static class SuccessMessage
        {
            // Female messages
            public static string FeUpdated { get; set; } = "Actualizada correctamente";
            public static string FeDelete { get; set; } = "Eliminada correctamente";
            public static string FeCreate { get; set; } = "Creada correctamente";

            // Male messages
            public static string MaUpdated { get; set; } = "Actualizado correctamente";
            public static string MaDelete { get; set; } = "Eliminado correctamente";
            public static string MaCreate { get; set; } = "Creado correctamente";

        }

        public static class ErrorMessage
        {
            public static string GenericError { get; set; } = "Ha ocurrido un error, por favor vuelva a intentarlo";
            public static string EmailAlreadyExits { get; set; } = "Esta direccion de correo ya se encuentra en uso";
        }
    }
}
