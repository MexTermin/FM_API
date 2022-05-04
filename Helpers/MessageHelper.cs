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
            public static string NameAlreadyExits { get; set; } = "Este nombre ya se encuentra en uso";
            public static string IncorrectMonth { get; set; } = "Número de mes incorrecto";
            public static string IncorrectYear { get; set; } = "El año seleccionado no puede ser menor al actuar";
            public static string YearAlreadyExits { get; set; } = "Este año ya se encuentra en uso";
        }
    }
}
