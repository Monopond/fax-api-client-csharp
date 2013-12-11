using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.ServiceModel;

namespace FaxApiV2
{
    class Program
    {

        static String PRODUCTION_URL = "https://api.monopond.com/fax/soap/v2/";
        static String TEST_URL = "https://test.api.monopond.com/fax/soap/v2/";

        static void Main(string[] args)
        {
            // TODO: change user credentials
            string username = "username";
            string password = "password";

            // Monopond Fax API Production URL
            //ApiService apiClient = new ApiService(PRODUCTION_URL, username, password);

            // Monopond Fax API Test URL
            ApiService apiClient = new ApiService(TEST_URL, username, password);

            sendFaxSample(apiClient);
            pauseFaxSample(apiClient);
            resumeFaxSample(apiClient);
            faxStatusSample(apiClient);
            stopFaxSample(apiClient);
        }

        private static void sendFaxSample(ApiService apiClient)
        {
            // create a new fax document.
            apiFaxDocument apiFaxDocument = new apiFaxDocument();
            apiFaxDocument.FileData = "VGhpcyBpcyBhIGZheA==";
            apiFaxDocument.FileName = "test.txt";

            // create an array of api fax documents.
            apiFaxDocument[] apiFaxDocuments;
            apiFaxDocuments = new apiFaxDocument[1] { apiFaxDocument };

            //create a new fax message.
            apiFaxMessage apiFaxMessage1 = new apiFaxMessage();
            apiFaxMessage1.MessageRef = "test-1-1-1";
            apiFaxMessage1.SendTo = "6011111111";
            apiFaxMessage1.SendFrom = "Test fax";
            apiFaxMessage1.Resolution = faxResolution.normal;
            apiFaxMessage1.Documents = apiFaxDocuments;

            // create another fax message.
            apiFaxMessage apiFaxMessage2 = new apiFaxMessage();
            apiFaxMessage2.MessageRef = "test-1-1-1";
            apiFaxMessage2.SendTo = "6011111111";
            apiFaxMessage2.SendFrom = "Test fax";
            apiFaxMessage2.Resolution = faxResolution.normal;
            apiFaxMessage2.Documents = apiFaxDocuments;

            // create an array of api fax messages.
            apiFaxMessage[] apiFaxMessages = new apiFaxMessage[2] { apiFaxMessage1, apiFaxMessage2 };

            //create a new instance of sendFax request.
            sendFaxRequest sendFaxRequest = new sendFaxRequest();
            sendFaxRequest.FaxMessages = apiFaxMessages;

            // call the sendFax method.
            sendFaxResponse sendFaxResponse = apiClient.SendFax(sendFaxRequest);
            Console.WriteLine("response: " + sendFaxResponse.ToString());
            //Console.ReadLine();
        }

        private static void pauseFaxSample(ApiService apiClient)
        {
            // create a new instance of pauseFax request.
            pauseFaxRequest pauseFaxRequest = new pauseFaxRequest();
            pauseFaxRequest.MessageRef = "test-1-1-1";

            // call the pauseFax method.
            pauseFaxResponse pauseFaxResponse = apiClient.PauseFax(pauseFaxRequest);
        }

        private static void resumeFaxSample(ApiService apiClient)
        {
            // create a new instace of resumeFax request.
            resumeFaxRequest resumeFaxRequest = new resumeFaxRequest();
            resumeFaxRequest.MessageRef = "test-1-1-1";

            // call the resumeFax method.
            resumeFaxResponse resumeFaxResponse = apiClient.ResumeFax(resumeFaxRequest);
        }

        private static void faxStatusSample(ApiService apiClient)
        {
            // create a new instance of fax status request.
            faxStatusRequest faxStatusRequest = new faxStatusRequest();
            faxStatusRequest.MessageRef = "test-1-1-1";
            faxStatusRequest.Verbosity = faxStatusLevel.brief;

            // call the faxStatus method.
            faxStatusResponse response = apiClient.FaxStatus(faxStatusRequest);
        }

        private static void stopFaxSample(ApiService apiClient)
        {
            // create a new instance of stopFax request.
            stopFaxRequest stopFaxRequest = new stopFaxRequest();
            stopFaxRequest.MessageRef = "test-1-1-1";

            // call the stopFax method.
            stopFaxResponse stopFaxResponse = apiClient.StopFax(stopFaxRequest);
        }
    }
}
