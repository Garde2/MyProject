namespace MyProjectL   //интерфейс (вариант как класс или структура)
                       //класс интерфейса имеет название поведения, ничего не реализовывает
                       //но все классы кто наследует обязаны иметь данный метод
{
    public interface ITakeDamage
    {
        public void Hurt(float _damage);
    }
}
