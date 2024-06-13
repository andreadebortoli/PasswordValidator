using FileHandler;
using Moq;
using Validator;
using Validator.Interfaces;

namespace PasswordValidationTests;

public class PasswordCheckerTests
{
    private IPasswordChecker _sut;
    private Mock<IWriter> _fileWriter;
    private Mock<IPasswordEncryptor> _passwordEncryptor;

    [SetUp]
    public void SetUp()
    {
        _fileWriter = new Mock<IWriter>();
        _passwordEncryptor = new Mock<IPasswordEncryptor>();
        _sut = new PasswordChecker(_fileWriter.Object, _passwordEncryptor.Object);
        
    }

    [Test]
    [TestCase("Password12!")]
    public void CallPasswordEncryptorAndFileWriterOnceIfPasswordIsValid(string password)
    {
        _sut.CheckPassword(true, password);

        _passwordEncryptor.Verify(pe => pe.EncryptPassword(It.IsAny<string>()), Times.Once);
        _fileWriter.Verify(fw => fw.WriteToFile(It.IsAny<string>()), Times.Once);

    }

    [Test]
    [TestCase("Passwor2!")]
    public void DoesntCallPasswordEncryptorAndFileWriterIfPasswordIsInValid(string password)
    {
        _sut.CheckPassword(false, password);

        _passwordEncryptor.Verify(pe => pe.EncryptPassword(It.IsAny<string>()), Times.Never);
        _fileWriter.Verify(m => m.WriteToFile(It.IsAny<string>()), Times.Never);

    }
}