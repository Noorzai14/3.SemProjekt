namespace BarberAkji.Web.Models
{
    // ViewModel der bruges til at vise fejl i Error.cshtml
    public class ErrorViewModel
    {
        // Gemmer ID'et for den aktuelle HTTP-request – bruges til fejlfinding
        public string? RequestId { get; set; }

        // Bruges i viewet til at vise fejlinfo hvis der faktisk er et ID
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
