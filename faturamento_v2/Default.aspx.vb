﻿Imports System
Imports System.Security
Imports System.Security.Cryptography
Imports System.Security.Permissions
Imports System.IO
Imports System.Security.Cryptography.X509Certificates

Public NotInheritable Class X509Certificate2UI
  'Usage
   Dim instance As X509Certificate2UI
End Class

Partial Class Municipio_faturamento_v2_Default
  Inherits System.Web.UI.Page
   'Declaration

End Class

Class CertSelect

    Shared Sub Main()
        Try 
            Dim store As New X509Store("MY", StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
            Dim collection As X509Certificate2Collection = CType(store.Certificates, X509Certificate2Collection)
            Dim fcollection As X509Certificate2Collection = CType(collection.Find(X509FindType.FindByTimeValid, DateTime.Now, False), X509Certificate2Collection)
            Dim scollection As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(fcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.MultiSelection)
            Console.WriteLine("Number of certificates: {0}{1}", scollection.Count, Environment.NewLine)
            Dim x509 As X509Certificate2
            For Each x509 In scollection
                Dim rawdata As Byte() = x509.RawData
                Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine)
                Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine)
                Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine)
                Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, True), Environment.NewLine)
                Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine)
                Console.WriteLine("Private Key: {0}{1}", x509.PrivateKey.ToXmlString(False), Environment.NewLine)
                Console.WriteLine("Public Key: {0}{1}", x509.PublicKey.Key.ToXmlString(False), Environment.NewLine)
                Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine)
                Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine)
                X509Certificate2UI.DisplayCertificate(x509)
                x509.Reset()
            Next x509
            store.Close()
        Catch cExcept As CryptographicException
            Console.WriteLine("Information could not be written out for this certificate.")
        End Try 

    End Sub 
  End Class