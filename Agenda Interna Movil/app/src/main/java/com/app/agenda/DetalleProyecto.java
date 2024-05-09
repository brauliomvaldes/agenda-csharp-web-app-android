package com.app.agenda;


import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.provider.Settings;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;


import java.util.HashMap;

public class DetalleProyecto extends AppCompatActivity {



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detalle_proyecto);

        TextView txtn;
        TextView txtfc;
        TextView txta;
        TextView txtm;
        TextView txtfp;
        TextView txtp;

        Bundle contrato = getIntent().getExtras();
        if(contrato!=null){
            try{
                Globales.setIdproyecto(contrato.getString("IdProyecto","IdProyecto"));

                String n = contrato.getString("NombreProyecto", "NombreProyecto");
                Globales.setNombreProyecto(n);
                String fc = contrato.getString("FechaCreacion","FechaCreacion");
                String a = contrato.getString("Aprobado","Aprobado");
                if(a.equals(Globales.verdadero)){
                    a="Proyecto Aprobado";
                }else{
                    a="Proyecto No Aprobado";
                }
                String m = contrato.getString("MontoProyecto","MontoProyecto");
                String fp = contrato.getString("FechaPago","FechaPago");
                String p = contrato.getString("Pagado","Pagado");
                if(p.equals(Globales.verdadero)){
                    p="Pagado";
                }else{
                    p="No registra pago";
                }

                txtn = (TextView) findViewById(R.id.textViewDatoC);
                txtfc = (TextView) findViewById(R.id.textViewDatoFC);
                txta = (TextView) findViewById(R.id.textViewDatoA);

                txtm = (TextView) findViewById(R.id.textViewDatoM);
                txtfp = (TextView) findViewById(R.id.textViewDatoFP);
                txtp = (TextView) findViewById(R.id.textViewDatoP);

                txtn.setText(n);
                txtfc.setText(fc);
                txta.setText(a);
                txtm.setText(m);
                txtfp.setText(fp);
                txtp.setText(p);
            }catch (Exception e){
                Toast.makeText(getApplicationContext(), "Error de lectura BD!", Toast.LENGTH_SHORT).show();
            }

        }else{
            Toast.makeText(getApplicationContext(), "No hay datos!!!", Toast.LENGTH_SHORT).show();
        }
    }

    public void buscarDocumentos(View view){
        Intent i = new Intent(getApplicationContext(), DocumentosProyecto.class);
        startActivity(i);

    }

}