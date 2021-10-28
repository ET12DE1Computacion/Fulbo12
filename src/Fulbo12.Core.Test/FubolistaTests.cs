using Xunit;

namespace Fulbo12.Core.Test
{
    [Collection("ColeccionPersonasFutbolistas")]
    public class FubolistaTests
    {
        FutbolistasFixture Fixture { get; set; }
        public FubolistaTests(FutbolistasFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void MismaNacionalidad()
        {
            Assert.False(Fixture.FFrankFabra.MismaNacionalidad(Fixture.FLioMessi));
            Assert.True(Fixture.FLioMessi.MismaNacionalidad(Fixture.FMarcosRojo));
        }

        [Fact]
        public void MismaLiga()
        {
            Assert.False(Fixture.FLioMessi.MismaLiga(Fixture.FMarcosRojo));
            Assert.True(Fixture.FMarcosRojo.MismaLiga(Fixture.FFrankFabra));
        }

        [Fact]
        public void MismoEquipo()
        {
            Assert.False(Fixture.FLioMessi.MismoEquipo(Fixture.FMarcosRojo));
            Assert.True(Fixture.FMarcosRojo.MismoEquipo(Fixture.FFrankFabra));
        }
        [Fact]
        public void JuegaDe()
        {
            Assert.True(Fixture.FLioMessi.JuegaDe(Fixture.MediocampistaOfensivo));
            Assert.False(Fixture.FLioMessi.JuegaDe(Fixture.DefensorCentral));
            Assert.True(Fixture.FFrankFabra.JuegaDe(Fixture.DefensorIzquierdo));
            Assert.False(Fixture.FFrankFabra.JuegaDe(Fixture.MediaPunta));
        }
    }
}
