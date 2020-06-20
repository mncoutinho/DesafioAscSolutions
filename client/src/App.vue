<template>
  <v-app id="inspire">
    <v-navigation-drawer
      v-model="drawer"
      app
      clipped
    >
       <v-list>    
          <v-list-item-content> 
            <v-btn 
            style="text-decoration:none;"
            v-for="items in navItens"
            :key="items.title"
            @click="items.funcao"
 
            >
              <v-col>
                <v-list-item-title v-text="items.title"/>
              </v-col>
            </v-btn>
          </v-list-item-content>          
        </v-list>

    </v-navigation-drawer>

    <v-app-bar
      app
      clipped-left
    >
  
      <v-toolbar-title>Simulador</v-toolbar-title>
    </v-app-bar>
    <v-main>
      <v-container
        class="fill-height"
        fluid>
        <v-row v-if="showturmas">
          <v-col
          v-for="turma in turmas"
          :key="turma.idTurma">
            <v-card v-if="turma[0]">
              <v-card-title>Turmas {{turma[0].idTurma}}</v-card-title>
              <v-card class="ma-2" v-for="aluno in turma"
              :key="aluno.id">
                <v-card-title>{{aluno.name}}</v-card-title>
                <v-card-subtitle>
                  <small>Nota 1:{{aluno.nota1}}</small>
                  <v-spacer></v-spacer>
                  <small>Nota 2:{{aluno.nota2}}</small>
                  <v-spacer></v-spacer>
                  <small>Nota 3:{{aluno.nota3}}</small>
                  <v-spacer></v-spacer>
                  <small>Média:{{aluno.media}}</small>
                  <v-spacer></v-spacer>
                  <small>Média Final:{{aluno.mediaFinal}}</small>
                  <v-spacer></v-spacer>
                  <small>Status:{{aluno.statusAluno}}</small>
                  </v-card-subtitle>
              </v-card>
            </v-card>
          </v-col>
       </v-row>
       <v-row v-if="showclassificados">
          <v-col
          v-for="classificado in classificados"
          :key="classificado.idTurma">
            <v-card v-if="classificado">
              <v-card class="ma-2">
                <v-card-title>{{classificado.name}}</v-card-title>
                <v-card-subtitle>
                  <small>Média:{{classificado.media}}</small>
                  <v-spacer></v-spacer>
                  <small>Média Final:{{classificado.mediaFinal}}</small>
                  <v-spacer></v-spacer>
                  <small>Status:{{classificado.statusAluno}}</small>
                  </v-card-subtitle>
              </v-card>
            </v-card>
          </v-col>
       </v-row>
       <v-row v-if="showganhador"
       align="center"
       justify="center">
            <v-card v-if="ganhador">
                <v-card-title>Ganhador: {{ganhador.aluno.name}}</v-card-title>
                <v-card-subtitle>
                  <small>Status: {{ganhador.aluno.statusAluno}}</small>
                  <v-spacer></v-spacer>
                  <small>Nota da Prova da Premiação: {{ganhador.provaPremio}}</small>
                   <v-spacer></v-spacer>
                   <small>Turma: {{ganhador.aluno.idTurma}}</small>
                </v-card-subtitle>

              
            </v-card>
       </v-row>
      </v-container>
    </v-main>

    <v-footer app>
      <span>&copy; 2020 Desafio ASC Solutions By Pedro Coutinho</span>
    </v-footer>
  </v-app>
</template>

<script>
import axios from 'axios';
  export default {
    props: {
      source: String,
    },
    data () {
      return{
        turmas:{
          turma1:[],
          turma2:[],
          turma3:[]
          },
          start:false,
          geraclassificados:false,
          geraGanhador:false,
          showturmas:false,
          showclassificados:false,
          showganhador:false,
        classificados:[],
        ganhador:{},
        drawer: null,
        navItens:[
            {  title: 'Começar a Simulação', funcao: this.startSimulador },
            {  title: 'Ver Turmas', funcao: this.getAlunos },
            {  title: ' Classificados Para Premio', funcao: this.getClassificados },
            {  title: ' Ganhador do Premio', funcao: this.getGanhador },
          ]

      }
    },
    created () {
      this.$vuetify.theme.dark = true
    },

    methods:{
      async startSimulador(){
        if(!this.start){
          this.start = true
          await axios.get('https://localhost:5001/Simulador/')
        }
      },
      async getAlunos(){
        this.showturmas = !this.showturmas

          await axios.get('https://localhost:5001/Simulador/getAll')
        .then(response => {
          this.turmas={turma1: [],turma2: [], turma3: []};
            response.data.forEach(aluno => {
              if(aluno.idTurma === 1){
                this.turmas.turma1.push(aluno)
              }
              if(aluno.idTurma === 2){
                this.turmas.turma2.push(aluno)
              }
              if(aluno.idTurma === 3){
                this.turmas.turma3.push(aluno)
              }
            })
          })

        
      },
      async getClassificados(){
        this.showclassificados = !this.showclassificados
        if(!this.geraclassificados){
          this.geraclassificados = true
          await axios.get('https://localhost:5001/Simulador/Premio/Classificados').then(
            response => {
              this.classificados = response.data
            }
        )}
        
      },
      async getGanhador(){
        this.showganhador = !this.showganhador
        if(!this.geraGanhador){
          this.ganhador = {}
          this.geraGanhador = true;
          await axios.get('https://localhost:5001/Simulador/Premio/Ganhador').then(
              response => {
                this.ganhador = response.data
              })
        }
      }
    }
  }
</script>