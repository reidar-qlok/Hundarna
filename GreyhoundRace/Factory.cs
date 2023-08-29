namespace GreyhoundRace
{
    public static class Factory
    {
        // Decides which class to instantiate
        public static Punter GetAPunter(int id)
        {
            switch (id)
            {
                case 0: return new Emely();
                case 1: return new Tobias();
                case 2: return new Reidar();
                default: return null;
            }
        }

    }
}
