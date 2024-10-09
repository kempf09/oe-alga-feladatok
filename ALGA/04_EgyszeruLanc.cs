using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek;

internal class LancElem<T>
{
    public T tart;
    public LancElem<T> kov;

    public LancElem(T tart, LancElem<T>? kov)
    {
        this.tart = tart;
        this.kov = kov;
    }
}


/// <summary>
///  LANCOLTVEREM -  LANCOLTVEREM -  LANCOLTVEREM - 
/// </summary>
/// <typeparam name="T"></typeparam>
public class LancoltVerem<T>: Verem<T>
{
    LancElem<T>? fej;

    public LancoltVerem()
    {
        fej = null;
    }

    public bool Ures
    {
        get
        {
            return fej == null;
        }
    }
    public void Verembe(T ertek)
    {
        LancElem<T> uj = new LancElem<T>(ertek, fej);
        fej = uj;
    }

    public T Verembol()
    {
        if (fej != null)
        {
            T ertek = fej.tart;
            //LancElem<T> q = fej;
            fej = fej.kov;
            return ertek;
        }
        else throw new NincsElemKivetel();
    }

    public T Felso()
    {
        if (fej!= null) return fej.tart;
        else throw new NincsElemKivetel();
    }
    
 
    
}

/// <summary>
/// LANCOLTSOR - LANCOLTSOR - LANCOLTSOR - LANCOLTSOR - 
/// </summary>
/// <typeparam name="T"></typeparam>

public class LancoltSor<T> : Sor<T>
{
    LancElem<T>? fej;
    LancElem<T>? vege;



    public bool Ures
    {
        get { return fej == null; }
    }

    public void Sorba(T ertek)
    {
        LancElem<T> uj = new LancElem<T>(ertek, null);
        if (vege != null) vege.kov = uj;
        else fej = uj;
        vege = uj;
    }

    public T Sorbol()
    {
        if (fej!=null)
        {
            T ertek = fej.tart;
            fej = fej.kov;
            if (fej==null) vege = null;
            return ertek;
            
        }
        else throw new NincsElemKivetel();
    }

    public T Elso()
    {
        if (fej != null) return fej.tart;
        else throw new NincsElemKivetel();
    }
}
/// <summary>
/// LANCOLTLISTA - LANCOLTLISTA - LANCOLTLISTA - LANCOLTLISTA
/// </summary>
/// <typeparam name="T"></typeparam>
public class LancoltLista<T> : Lista<T>, IEnumerable<T>
{
    
    LancElem<T>? fej;
    LancElem<T>? p;

    public int Elemszam
    {
        get
        {
            int n = 0;
            LancElem<T> p;
            p = fej;
            if(fej==null) return 0;
            else
            {
                while (p != null)
                {
                    
                    p=p.kov;
                    n++;
                }
                return n;
            }
            
            
        }
    }

    public LancoltLista()
    {
        fej = null;
    }
    public T Kiolvas(int index)
    {
        var p = fej;
        int i = 0;

        while (p!=null && i<index)
        {
            p = p.kov;
            i++;
        }
        if(p!=null) return p.tart;
        else throw new HibasIndexKivetel();


    }

    public void Modosit(int index, T ertek)
    {
        var p = fej;
        int i = 0;

        while (p!=null  && i<index)
        {
            p= p.kov;
            i++;
        }
        if(p!=null) p.tart = ertek;
        else throw new HibasIndexKivetel();
    }

    public void Hozzafuz(T ertek)
    {
        LancElem<T> p;
        var uj = new LancElem<T>(ertek, null);
        if (fej == null)
        {
            fej = uj;
        }
        else
        {
            p = fej;
            while (p.kov != null)
            {
                p=p.kov;
            }
            p.kov = uj;
        }
    }

    public void Beszur(int index, T ertek)
    {
        LancElem<T> p;
        if (fej == null || index ==0)
        {
            var uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }
        else
        {
            p = fej;
            int i = 1;
            while (p.kov!=null && i<index)
            {
                p = p.kov;
                i++;
            }

            if (i <= index)
            {
                LancElem<T>? uj = new LancElem<T>(ertek, p.kov);
                p.kov = uj;
            }
            else throw new HibasIndexKivetel();
        }
    }

    public void Torol(T ertek)
    {
        var p = fej;
        LancElem<T> e = null;
        LancElem<T>? q;
        do
        {
            while (p!=null && !p.tart.Equals(ertek) )
            {
                e = p;
                p=p.kov;
            }

            if (p!=null)
            {
                q = p.kov;
                if (e == null)
                {
                    fej = q;
                }
                else
                {
                    e.kov = q;
                }

                p = q;
            }
            
            
        } while (p!=null);
    }

    public void Bejar(Action<T> muvelet)
    {
        LancElem<T> p;
        p = fej;

        while (p != null) 
        {
            muvelet(p.tart);
            p=p.kov;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new LancoltListaBejaro<T>(fej);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class LancoltListaBejaro<T>: IEnumerator<T>
{
    public LancElem<T>? fej;
    private LancElem<T>? current;

    public LancoltListaBejaro(LancElem<T>? fej)
    {
        this.fej = fej;
        current = null;
    }
    
    public bool MoveNext()
    {
        if (current == null) current = fej;
        else current = current.kov;
        
        return current != null;
    }

    public void Reset()
    {
        current=null;
    }

    public T Current
    {
        get
        {
            return current.tart;
        }
    }

    object? IEnumerator.Current => Current;

    public void Dispose()
    {
        
    }
}