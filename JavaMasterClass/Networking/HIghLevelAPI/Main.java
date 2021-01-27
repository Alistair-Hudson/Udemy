package HIghLevelAPI;

import java.io.BufferedReader;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import java.util.Map;
import java.io.InputStreamReader;

public class Main {
    
    public static void main(String[] args) {
        
        try{

            URL url = new URL("http://example.org");
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");
            connection.setRequestProperty("User-Agent", "Chrome");
            connection.setReadTimeout(30000);

            int responseCode = connection.getResponseCode();
            System.out.println("Response code " + responseCode);
            if(responseCode != 200){
                System.out.println("Error reading page");
                return;
            }
            BufferedReader inputStream = new BufferedReader(
                                            new InputStreamReader(connection.getInputStream()));
            
            String line = "";

            while((line = inputStream.readLine()) != null){
                System.out.println(line);
            }
            inputStream.close();


            // URLConnection urlConnection = url.openConnection();
            //Write all configuration now
            // urlConnection.setDoOutput(true);

            // //No more configuration
            // urlConnection.connect();

            // // URI uri = url.toURI();
            
            // BufferedReader inputStream = new BufferedReader(
            //                                 new InputStreamReader(urlConnection.getInputStream()));
            
            // Map<String, List<String>> headerFields = urlConnection.getHeaderFields();
            // for(Map.Entry<String, List<String>> entry : headerFields.entrySet()){
            //     String key = entry.getKey();
            //     List<String> value = entry.getValue();
            //     System.out.println("----key = " + key);
            //     for (String string :value){
            //         System.out.println("value = " + value);
            //     }
            // }

            // String line = "";

            // while(line != null){
            //     line = inputStream.readLine();
            //     System.out.println(line);
            // }
            // inputStream.close();


            // URI uri = new URI("dbv://username:password@myserver.com:5000/catalogue/phones?os=android#samsung");
            // URI baseURI = new URI("http://username:password@myserver.com:5000");
            // URI uri1 = new URI("/catalogue/phones?os=android#samsung");
            // URI uri2 = new URI("/catalogue/tvs?manufacturer=samsung");
            // URI uri3 = new URI("/stores/locations?zip=12345");
            // URI resolvedUri1 = baseURI.resolve(uri1);
            // URI resolvedUri2 = baseURI.resolve(uri2);
            // URI resolvedUri3 = baseURI.resolve(uri3);

            // URL url1 = resolvedUri1.toURL();
            // System.out.println("URL = " + url1);
            // URL url2 = resolvedUri2.toURL();
            // System.out.println("URL = " + url2);
            // URL url3 = resolvedUri3.toURL();
            // System.out.println("URL = " + url3);

            // URI relativizedURI = baseURI.relativize(resolvedUri1);
            // System.out.println("Relative URI " + relativizedURI);
            
            // System.out.println("Scheme = " + uri.getScheme());
            // System.out.println("Scheme-Speific port = " + uri.getSchemeSpecificPart());
            // System.out.println("Authority = " + uri.getAuthority());
            // System.out.println("User = " + uri.getUserInfo());
            // System.out.println("Host = " + uri.getHost());
            // System.out.println("Port = " + uri.getPort());
            // System.out.println("Path = " + uri.getPath());
            // System.out.println("Query = " + uri.getQuery());
            // System.out.println("Fragment = " + uri.getFragment());


        // }catch(URISyntaxException e){
        //     System.out.println("Bad syntax " + e.getMessage());
        }catch(MalformedURLException e){
            System.out.println("URL malformed " + e.getMessage());
        }catch(IllegalArgumentException e){
            System.out.println("Illeagl argument " + e.getMessage());
        }catch(IOException e){
            System.out.println("IOException " +e.getMessage());
        }
    }
}
