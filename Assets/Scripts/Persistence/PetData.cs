public class PetData
{
    int hunger, energy, happiness, cleanliness;

    public PetData(PetController pet) {
        hunger = pet.hunger;
        energy = pet.energy;
        happiness = pet.happiness;
        cleanliness = pet.cleanliness;
    }
}