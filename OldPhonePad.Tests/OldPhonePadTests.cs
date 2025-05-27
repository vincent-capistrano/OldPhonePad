using Xunit;

public class OldPhonePadTests
{
    [Theory]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("222 2 22#", "CAB")]
    [InlineData("777733266#", "SEAN")]
    [InlineData("44 444#", "HI")]
    [InlineData("8 88 777 444 66 4#", "TURING")]
    [InlineData("2#", "A")] 
    public void SimulateInput_ReturnsExpectedOutput(string input, string expected)
    {
        var simulator = new OldPhonePadTestSim();
        string actual = simulator.Simulate(input);
        Assert.Equal(expected, actual);
    }
}
