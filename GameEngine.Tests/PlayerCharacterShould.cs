using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould: IDisposable
    {
        private readonly PlayerCharacter _sut;
        private readonly ITestOutputHelper _output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Creating new PlayerCharacter");
            _sut = new PlayerCharacter();
        }

        public void Dispose()
        {
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");

            // _sut.Dispose();
        }

        [Fact]
        public void BeInexperiencedWhenNew()
        {
            Assert.True(_sut.IsNoob);
        }

        // ********************* String Asserts - Start ****************************
        [Fact]
        public void CalculateFullName()
        {
            _sut.FirstName = "Shubham";
            _sut.LastName = "Shukla";

            Assert.Equal("Shubham Shukla", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameStartsWithFirstName()
        {
            _sut.FirstName = "Shubham";

            Assert.StartsWith("Shubham", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameEndsWithLastName()
        {
            _sut.LastName = "Shukla";

            Assert.EndsWith("Shukla", _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnoreCaseAssertExample()
        {
            _sut.FirstName = "Shubham";
            _sut.LastName = "Shukla";

            Assert.Equal("Shubham Shukla", _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CalculateFullName_SubstringAssertExample()
        {
            _sut.FirstName = "Shubham";
            _sut.LastName = "Shukla";

            Assert.Contains("am Sh", _sut.FullName);
        }

        [Fact]
        public void CalculateFullNameWithTitleCase()
        {
            _sut.FirstName = "Shubham";
            _sut.LastName = "Shukla";

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
        }

        // ********************* String Asserts - End ****************************

        // ********************* Numeric Asserts - Start ****************************

        [Fact]
        public void StartsWithDefaultHealth()
        {
            Assert.Equal(100, _sut.Health);
        }

        [Fact]
        public void StartsWithDefaultHealth_NotEqualExample()
        {
            Assert.NotEqual(0, _sut.Health);
        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            _sut.Sleep();

            //Assert.True(_sut.Health >= 101 && _sut.Health <= 200);
            //Assert.InRange<int>(_sut.Health, 101, 200);
            Assert.InRange(_sut.Health, 101, 200);
        }

        // ********************* Numeric Asserts - End ****************************

        // ********************* Null Asserts - Start ****************************

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            Assert.Null(_sut.Nickname);
            // Assert.NotNull(_sut.Nickname);
        }

        // ********************* Null Asserts - End ****************************


        // ********************* Collection Asserts - Start ****************************

        [Fact]
        public void HaveaLongBow()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
        }

        [Fact]
        public void NotHaveAStaffOfWonder()
        {
            Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
        }

        [Fact]
        public void HaveAtleastOneKindOfSword()
        {
            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, _sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons()
        {
            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        // ********************* Collection Asserts - End ****************************

        // ********************* Events Raised Asserts - Start ****************************

        [Fact]
        public void RaiseSleptEvent()
        {
            // checks that when Sleep function is called, Player Slept events are raised or not
            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));
        }

        // ********************* Events Raised Asserts - End ****************************

        //[Fact]
        //public void TakeZeroDamage()
        //{
        //    _sut.TakeDamage(0);
        //    Assert.Equal(100, _sut.Health);
        //}

        //[Fact]
        //public void TakeSmallDamage()
        //{
        //    _sut.TakeDamage(1);
        //    Assert.Equal(99, _sut.Health);
        //}

        //[Fact]
        //public void TakeMediumDamage()
        //{
        //    _sut.TakeDamage(50);
        //    Assert.Equal(50, _sut.Health);
        //}

        //[Fact]
        //public void HaveMinimumHealth()
        //{
        //    _sut.TakeDamage(101);
        //    Assert.Equal(1, _sut.Health);
        //}

        //[Theory]
        //[InlineData(0,100)]
        //[InlineData(1, 99)]
        //[InlineData(50, 50)]
        //[InlineData(101, 1)]
        //public void TakeDamage(int damage, int expectedHealth)
        //{
        //    _sut.TakeDamage(damage);
        //    Assert.Equal(expectedHealth, _sut.Health);
        //}

        //[Theory]
        //[MemberData(nameof(InternalHealthDamageTestData.TestData),
        //    MemberType = typeof(InternalHealthDamageTestData))]
        //public void TakeDamage(int damage, int expectedHealth)
        //{
        //    _sut.TakeDamage(damage);
        //    Assert.Equal(expectedHealth, _sut.Health);
        //}

        [Theory]
        [HealthDamageData]
        public void TakeDamage(int damage, int expectedHealth)
        {
            NonPlayerCharacter sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
    }
}
 