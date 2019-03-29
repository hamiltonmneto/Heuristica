import {HttpClient, json} from 'aurelia-fetch-client';

let httpClient = new HttpClient();

export class App {
  numbers = ['1', '2', '3', '4', '5', '6', '7', '8',' '];
  
  constructor() {
    this.numbers = this.shuffle(this.numbers);
    httpClient.configure(config => {
      config
          .withBaseUrl('/')
          .withDefaults({
              headers: {
                  'Accept': 'application/json',
                  'X-Requested-With': 'Fetch'
              }
          })
    });
  }


  shuffle(array) {
    var currentIndex = array.length, temporaryValue, randomIndex;
  
    while (0 !== currentIndex) {
  
      randomIndex = Math.floor(Math.random() * currentIndex);
      currentIndex -= 1;
  
      temporaryValue = array[currentIndex];
      array[currentIndex] = array[randomIndex];
      array[randomIndex] = temporaryValue;
    }
  
    return array;
  }

  solve(){
    this.sendState(this.numbers);
  }

  sendState(state){
    httpClient.fetch('https://localhost:44360/v1/NewState',{
      method: "post",
      body: JSON.stringify(state)
    })
      .then(response => response.json())
      .then(data => {
        this.numbers = data;
        console.log(data);
      });
    ;
  }

}


