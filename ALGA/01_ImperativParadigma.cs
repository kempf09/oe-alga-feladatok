
    
using System.Collections;

namespace OE.ALGA.Paradigmak;
    

    //Interfész létrehozása
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }

    //TÁROLÓ Osztály létrehozása
    public class FeladatTarolo<T>: IEnumerable<T> where T : IVegrehajthato
    {
        public int n;
        public T[] tarolo;

        //Konstruktor
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }

        //Check hogy van e hely és ha van insert
        public void Felvesz(T elem)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = elem;
                n++;
            }
            else if (n >= tarolo.Length)
            {
                throw new TaroloMegteltKivetel();
            }
        }

        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            FeladatTaroloBejaro<T> bejaro = new FeladatTaroloBejaro<T>(tarolo,n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    //IFuggo INterfész

    public interface IFuggo
    {
        bool FuggosegTeljesul { get; }
    }


    //Kivételkezelés
    public class TaroloMegteltKivetel : Exception
    {
        public TaroloMegteltKivetel():base("A tároló megtelt!"){}
    }
    
    



/// <summary>
/// BEjáróóóóóóóó
/// </summary>

    class FeladatTaroloBejaro<T>: IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisIndex = -1;
        IEnumerator<T> _iEnumeratorImplementation;


        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        public T Current
        {
            get
            {
                return tarolo[aktualisIndex];
            }
        }

        public bool MoveNext()
        {
            if (aktualisIndex < n-1)
            {
                aktualisIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void Reset()
        {
            aktualisIndex = -1;
        }

        object? IEnumerator.Current => ((IEnumerator)_iEnumeratorImplementation).Current;

        public void Dispose()
        {
            
        }
    }

    //FüggőFeladatTároló
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int size) : base(size) { }

        public override void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                if (tarolo[i].FuggosegTeljesul)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
    }




