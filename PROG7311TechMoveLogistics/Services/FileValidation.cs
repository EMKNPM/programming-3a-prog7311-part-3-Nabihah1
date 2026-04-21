namespace PROG7311TechMoveLogistics.Services
{
    public class FileValidations
    {
        //this class has nothing to do with the code 
        //its just for testing purporses 
        // it vefies that  the app only accepts .pdf formatted files 
        // so that we can run proper unit tests later on 

        public void ValidatePdfFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty.");

            var extension = Path.GetExtension(fileName).ToLower();

            if (extension != ".pdf")
                throw new InvalidOperationException("Only PDF files are allowed.");
        }

    }
}
