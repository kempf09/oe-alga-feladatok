using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    /// <summary>
    /// VEREM/// VEREM/// VEREM/// VEREM/// VEREM/// VEREM/// VEREM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TombVerem<T>: Verem<T>
    {
        public T[] E;
        int n = 0;

        public TombVerem(int meret)
        {
            E = new T[meret];
            
        }

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }
        public void Verembe(T ertek)
        {
            if (n < E.Length)
            {
                E[n]=ertek;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Verembol()
        {
            if (!Ures)
            {
                
                return E[--n];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public T Felso()
        {
            if (!Ures) return E[n - 1];
            else throw new NincsElemKivetel();
        }
    }
    
    /// <summary>
    /// TOMBSOR/// TOMBSOR/// TOMBSOR/// TOMBSOR/// TOMBSOR/// TOMBSOR
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TombSor<T>: Sor<T>
    {
        public T[] E;
        public int n=0; //A sorban lévő elemek aktuális száma.
        public int u; //Az utolsó beszúrt elem indexe (0, mivel még nem volt ilyen).
        public int e; //A legutóbb kiolvasott elem indexe (0, mivel még nem volt ilyen).
        public bool Ures
        {
            get{return n == 0;}
        }

        public TombSor(int meret)
        {
            E = new T[meret];
            n = 0;
            u = 0;
            e = 0;

        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                n++;
                
                
                u = ((u+1) % E.Length);
                E[u] = ertek;
                

            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        
        public T Sorbol()
        {
            
            if (n>0)
            {
                n--;
                e= ((e+1) % E.Length);
                
                return E[e];
                
            }
            else
               
                throw new NincsElemKivetel();
            
        }

        public T Elso()
        {
            if (n>0)
            {
                return E[((e+1) % E.Length)];
            }
            else throw new NincsElemKivetel();
        }
    }
    /// <summary>
    /// TOMBLISTA - TOMBLISTA - TOMBLISTA -TOMBLISTA - TOMBLISTA- TOMBLISTA
    /// </summary>
    
    public class TombLista<T> : Lista <T>, IEnumerable
    {

        public T[] E;
        public int n;
        public const int DefaultCapacity = 4;
      

       
        public TombLista()
        {
            E = new T[DefaultCapacity];
            n = 0;
        }


        public int Elemszam
        {
            get
            {
                int elemszam = 0;
                for (int i = 0; i < E.Length; i++)
                {
                    if (E[i] != null) elemszam++;
                }
                return elemszam;
                
            }
        }

        public void CapIncrease()
        {
            T[] newE = new T[E.Length * 2];
            Array.Copy(E, newE, E.Length);
            E=newE;

        }
        public T Kiolvas(int index)
        {
            if (index < n) return E[index];
            else throw new HibasIndexKivetel();
        }

        public void Modosit(int index, T ertek)
        {
            if (index <= n) E[index] = ertek;
            else throw new HibasIndexKivetel();
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n , ertek);
        }

        public void Beszur(int index, T ertek)
        {
            if (index < 0 )
            {
                throw new HibasIndexKivetel();
            }

            if (n == E.Length)
            {
                CapIncrease();
            }

            for (int i = n; i >index  ; i--)
            {
                E[i] = E[i-1];
            }
            E[index] = ertek;
            n++;
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0; i < n; i++)
            {
                if (ertek.Equals(E[i])) db++;
                else E[i-db]= E[i];
            }

            n = n - db;
            //miert nem commmitol
        }

        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }


        public IEnumerator GetEnumerator()
        {
            return E.GetEnumerator();
        }
    }

    // public class TombListaBejaro<T> : IEnumerator<T>
    // {
    //     private TombLista<T> lista;
    //     private int position = -1;
    //
    //     public TombListaBejaro(TombLista<T> lista)
    //     {
    //         this.lista = lista;
    //     }
    //     
    //     
    //     public bool MoveNext()
    //     {
    //         if (position < lista.Elemszam - 1)
    //         {
    //             position++;
    //             return true;
    //         }
    //
    //         return false;
    //     }
    //
    //     public void Reset()
    //     {
    //         position = -1;
    //     }
    //
    //     public T Current
    //     {
    //         
    //         get
    //         {
    //             if (position < 0 || position >= lista.Elemszam)
    //             {
    //                 throw new InvalidOperationException();
    //             }
    //             else return lista.E[position];
    //             
    //         }
    //     }
    //
    //     object? IEnumerator.Current => Current;
    //
    //     public void Dispose(){}
    //     
    // }
}