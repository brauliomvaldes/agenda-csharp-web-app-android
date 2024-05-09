package com.app.agenda;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import java.sql.Date;


public class DocumentosProyecto extends AppCompatActivity {


    private JsonArrayRequest request;
    private RequestQueue requestQueue;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_documentos_proyecto);
        String URL = Globales.protocolo+ Globales.miIP+Globales.linkDocumentosDelProyecto+Globales.getIdProyecto();

        TextView titulo = (TextView) findViewById(R.id.TituloContrato);
        titulo.setText("Proyecto : "+Globales.getNombreProyecto());

        request = new JsonArrayRequest(URL, new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                JSONObject jsonObject = null;

                if(response.length()==0){
                    Toast.makeText(getApplicationContext(), "No cuenta con Proyectos a cargo que mostrar", Toast.LENGTH_LONG).show();
                }else{
                    String fecha = "";
                    String documentos [] = new String[response.length()];

                    for (int i = 0; i < response.length(); i++) {

                        try {

                            jsonObject = response.getJSONObject(i);

                            fecha = jsonObject.getString("FechaSubida");
                            fecha = Globales.FechaLeidaToString(fecha);

                            documentos[i] = fecha+" : "+jsonObject.getString("NombreTipoDocumento")+
                                    " : "+jsonObject.getString("Descripcion")+"."+jsonObject.getString("NombreExtension");

                        } catch (JSONException e) {
                            e.printStackTrace();
                            Toast.makeText(getApplicationContext(), "error e:"+e, Toast.LENGTH_LONG).show();
                        }
                    }
                    despliege(documentos);
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), "Contrato sin documentos", Toast.LENGTH_LONG).show();
            }
        });

        request.setRetryPolicy(new DefaultRetryPolicy( 0, -1, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(request);
    }

    public void despliege(String [] documentos){
        final ListView lista = (ListView)findViewById(R.id.ListView);
        ArrayAdapter<String> adapter = new ArrayAdapter<String>
                (this, android.R.layout.simple_list_item_1, documentos);
        lista.setAdapter(adapter);
        // accion sobre click en la lista

        lista.setOnItemClickListener(new AdapterView.OnItemClickListener(){
            public void onItemClick(AdapterView<?> parent, View view, int position, long id){

                // por definir accion a realizar
                int item = position;
                String itemval = (String)lista.getItemAtPosition(position);
                Toast.makeText(getApplicationContext(), "Documento:"+itemval, Toast.LENGTH_LONG).show();
            }
        });
    }

}