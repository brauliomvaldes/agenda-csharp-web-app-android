package com.app.agenda;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import java.util.ArrayList;
import java.util.HashMap;
import androidx.appcompat.widget.Toolbar;
import androidx.recyclerview.widget.RecyclerView;
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



public class Proyectos extends AppCompatActivity {

    private String URL = Globales.protocolo+ Globales.miIP+Globales.linkProyectosDelUsuario;
    private RecyclerView recyclerView;
    private RecyclerView.Adapter adapter;
    private JsonArrayRequest request;
    private RequestQueue requestQueue;

    private String contratos [][];

    Toolbar toolbar;
    boolean cierra = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_proyectos);

        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        String id_usuario=Globales.getId();
        String nombre =Globales.getNombre();
        String apellido =Globales.getApellido();
        String id_tipo_usuario =Globales.getTipoUsuario();

        if(id_usuario!=null){

            URL=URL+id_usuario+"&idtipousuario="+id_tipo_usuario;
            Globales.list = new ArrayList<>();

            poblarproyectosdelusuario();

        }else{
            Toast.makeText(getApplicationContext(), "No hay datos!!!", Toast.LENGTH_SHORT).show();
        }

    }

    public void poblarproyectosdelusuario(){

        request = new JsonArrayRequest(URL, new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                JSONObject jsonObject = null;

                int p=0;
                HashMap temp = new HashMap();

                if(response.length()==0){
                    Toast.makeText(getApplicationContext(), "No cuenta con Proyectos a cargo que mostrar", Toast.LENGTH_LONG).show();
                }else{

                    contratos = new String[response.length()][8];

                    temp = new HashMap();
                    temp.put("COLUMNA_1","PROYECTO");
                    temp.put("COLUMNA_2", "EMPRESA");
                    temp.put("COLUMNA_3", "U.F.");

                    Globales.list.add(temp);
                    for (int i = 0; i < response.length(); i++) {
                        p++;
                        try {

                            jsonObject = response.getJSONObject(i);

                            temp = new HashMap();

                            temp.put("COLUMNA_1",jsonObject.getString("NombreProyecto"));
                            temp.put("COLUMNA_2", jsonObject.getString("NombreEmpresa"));
                            temp.put("COLUMNA_3", jsonObject.getString("MontoProyecto"));

                            Globales.list.add(temp);

                            contratos [i][0] = jsonObject.getString("NombreProyecto");
                            contratos [i][1] = jsonObject.getString("NombreEmpresa");
                            contratos [i][2] = jsonObject.getString("MontoProyecto");
                            contratos [i][3] = Globales.FechaLeidaToString(jsonObject.getString("FechaCreacion"));
                            contratos [i][4] = jsonObject.getString("Aprobado");
                            contratos [i][5] = Globales.FechaLeidaToString(jsonObject.getString("FechaPago"));
                            contratos [i][6] = jsonObject.getString("Pagado");
                            contratos [i][7] = jsonObject.getString("IdProyecto");

                        } catch (JSONException e) {
                            e.printStackTrace();
                            Toast.makeText(getApplicationContext(), "error e:"+e, Toast.LENGTH_LONG).show();
                        }
                    }
                    despliege(Globales.list);
                }
            }


        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(), "Sin contratos a cargo que mostrar", Toast.LENGTH_LONG).show();
            }
        });

        request.setRetryPolicy(new DefaultRetryPolicy( 0, -1, DefaultRetryPolicy.DEFAULT_BACKOFF_MULT));
        requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(request);
    }

    public void despliege(ArrayList<HashMap> listado){

        ListView listview = (ListView) findViewById(R.id.listview);
        ListviewAdapter adapter = new ListviewAdapter(this, Globales.list);
        if (Globales.list!=null) {

            listview.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                @Override
                public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                    if(position>0) {
                        Intent i = new Intent(getApplicationContext(), DetalleProyecto.class);

                        position--;
                        i.putExtra("NombreProyecto", contratos[position][0]);
                        i.putExtra("NombreEmpresa", contratos[position][1]);
                        i.putExtra("MontoProyecto", contratos[position][2]);
                        i.putExtra("FechaCreacion", contratos[position][3]);
                        i.putExtra("Aprobado", contratos[position][4]);
                        i.putExtra("FechaPago", contratos[position][5]);
                        i.putExtra("Pagado", contratos[position][6]);
                        i.putExtra("IdProyecto", contratos[position][7]);
                        startActivity(i);
                    }
                }
            });

            listview.setAdapter(adapter);
        }else{
            Toast.makeText(getApplicationContext(), "no hay datos que mostrar"+Globales.list, Toast.LENGTH_LONG).show();
        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        return  true;
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        int id = item.getItemId();

        if(id==R.id.opcion1){
            Globales.setId(null);
            cierra=true;
            this.finish();
        }
        return true;
    }

    public void cerrar(View view){
        Globales.setId(null);
        finish();
    }
}