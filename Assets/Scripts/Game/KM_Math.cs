using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KM_Math : MonoBehaviour {

    static int KM_ChangeFlagTimer_Timer = 3599;

	public bool KM_ChangeFlagTimer(int l_Timer)
    {
        bool l_Flag = true;
        if (l_Flag)
        {
            if(KM_ChangeFlagTimer_Timer%l_Timer == 0)
            {
                l_Flag = false;
            }            
        }
        else
        {
            if (KM_ChangeFlagTimer_Timer % l_Timer == 0)
            {
                l_Flag = true;
            }
        }

        KM_ChangeFlagTimer_Timer--;

        if (KM_ChangeFlagTimer_Timer < 0)
        {
            KM_ChangeFlagTimer_Timer = 3599;
        }

        return l_Flag;
    }
}
