// Call_CSharp_COM.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#import "..\ComLibrary\ComLibrary.tlb" named_guids raw_interfaces_only

int _tmain(int argc, _TCHAR* argv[])
{
	CoInitialize(NULL);
    comlibrary::IMathPtr pDotNetCOMPtr;

	HRESULT hRes = pDotNetCOMPtr.CreateInstance(comlibrary::CLSID_Math);
	if (hRes == S_OK)
	{
        long res = 0L;
		hRes = pDotNetCOMPtr->Add(1, 2, &res);
	    if (hRes == S_OK)
	    {
            printf("The result was: %ld", res);
        }

        pDotNetCOMPtr.Release();
	}
	
	CoUninitialize ();
	return 0;
}

