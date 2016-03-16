using System;
using System.Windows;

namespace CapstoneCDCatalog
{
    public class DisplayExceptions
    {
        public static void DisplayException(Exception ex)
        {
            MessageBox.Show($"There was an problem with: {ex.InnerException}");
        }

        public static void DisplayNullReference(NullReferenceException ex)
        {
            MessageBox.Show($"Something went wrong with a null Reference: {ex.InnerException}");
        }
    }
}