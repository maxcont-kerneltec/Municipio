Imports Microsoft.VisualBasic

Public Class clsCalculaMod
  Public Sub New()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="strNumero"></param>
  ''' <returns></returns>
  Public Function Mod11(ByVal strNumero As String) As Integer
    'https://groups.google.com/forum/#!topic/sped-nfe/5VasOPIqM84

    Dim I As Integer : Dim IntCont As Integer : Dim Vlr As Integer
    Dim Resto As Integer
    Dim calculo As Integer

    IntCont = 2
    Vlr = 0

    For I = Len(strNumero) To 1 Step -1
      Vlr = Vlr + (Val(Mid(strNumero, I, 1) * IntCont))
      IntCont = IIf(IntCont >= 9, 2, IntCont + 1)
    Next

    Resto = Vlr Mod 11

    Select Case Resto
      Case 0
        Resto = 0
      Case 1
        Resto = 0
      Case Is > 1
        Resto = Str(Val(11 - Resto))
    End Select

    calculo = Resto

    Return calculo

  End Function

End Class
