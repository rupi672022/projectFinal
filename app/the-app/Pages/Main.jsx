import { View, Text, ImageBackground, StyleSheet, Image, TouchableOpacity } from 'react-native'
import React, { useState } from 'react'
import { Overlay, Icon } from 'react-native-elements';
import { TextInput } from 'react-native-paper';
import image from '../Images/logo.jpg';
import imagelogo from '../Images/yakobs.png';

export default function Main(props) {

  //date
  var today = new Date(),
    date = today.getDate() + '/0' + (today.getMonth() + 1) + '/' + today.getFullYear();
  const currentDate = date;

  const [workerNum, setWorkerNum] = useState(0); //num of worker

  const apiUrlEmploye = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Employes?employeNum=";

  const apiUrlOrder = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Orders?preparationDate=";

  //Overlay
  const [visible, setVisible] = useState(false);
  const toggleOverlay = () => {
    setVisible(!visible);
  }

  const btnCheckNum = () => { //check the num of worker
    fetch(apiUrlEmploye + workerNum, {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json ; charset=UTP-8',
        'Accept': 'application/json ; charset=UTP-8'
      })
    })
      .then(res => {
        return res.json();
      })
      .then(
        (result) => {
          if (result.length == 0) {
            toggleOverlay();
          }
          else { getOrders(result[0].EmployeNum) };
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const getOrders = (id) => {//get the num of order to this employe
    fetch(apiUrlOrder + currentDate + "&id=" + id, {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json ; charset=UTP-8',
        'Accept': 'application/json ; charset=UTP-8'
      })
    })
      .then(res => {
        return res.json();
      })
      .then(
        (result) => {
          props.navigation.navigate('OpenOrders', { orderArr: result, workerNum: workerNum });
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  return (
    <View style={styles.view}>
      <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
        <Text style={styles.logo}>משק יעקבס</Text>
        <Text style={styles.dateText}>{currentDate}</Text>
        <Text style={styles.headText}>ברוכים הבאים</Text>
      </ImageBackground>

      <Text style={styles.text}>הכנס את מספר העובד שלך : </Text>
      <TextInput
        keyboardType='numeric'
        style={styles.inputText}
        onChangeText={newText => setWorkerNum(newText)}
      />

      <View>
        <TouchableOpacity onPress={btnCheckNum}>
          <Text
            style={styles.buttonText} >קדימה לעבודה !</Text>
        </TouchableOpacity>

        <Overlay isVisible={visible} onBackdropPress={toggleOverlay}>
          <Icon name='warning' color='#ff4500' size={30} />
          <Text style={{ textAlign: 'center', fontSize: 18 }}>מספר עובד אינו תקין, נסה שנית</Text>
          <TouchableOpacity onPress={toggleOverlay}>
            <Text style={styles.buttonOvarlay}>אישור </Text>
          </TouchableOpacity>
        </Overlay>
      </View>

      <Image source={imagelogo} style={styles.yakobsimg}></Image>

    </View>
  )
}

const styles = StyleSheet.create({
  view: {
    backgroundColor: 'white',
    height: '100%'
  },
  imageback: {
    width: '140%',
    height: 400,
  },
  logo: {
    fontSize: 15,
    color: 'black',
    fontWeight: 'bold',
    marginLeft: 290,
    marginTop: 50,
  },
  dateText: {
    fontSize: 15,
    color: 'black',
    marginLeft: 290,
  },
  headText: {
    fontSize: 50,
    fontWeight: 'bold',
    color: 'black',
    marginLeft: 90,
    marginTop: 30
  },
  text: {
    fontSize: 20,
    color: 'black',
    marginTop: -100,
    marginBottom: 10,
    marginLeft: 150,
  },
  inputText: {
    marginLeft: 130,
    width: 200,
    height: 40,
    marginTop: 10,
    backgroundColor: '#FFE587',
    textAlign: 'center'
  },
  buttonText: {
    fontSize: 30,
    borderColor: '#FFE587',
    color: 'black',
    borderWidth: 2,
    borderRadius: 25,
    marginLeft: 130,
    marginTop: 30,
    marginRight: 40,
    padding: 10,
    textAlign: 'center'
  },
  buttonOvarlay: {
    fontSize: 20,
    marginTop: 10,
    textAlign: 'center',
    borderWidth: 2,
    backgroundColor: '#FFE587',
    width: 50,
    marginLeft: 70,
    borderColor: '#FFE587',
  },
  yakobsimg: {
    marginTop: 50,
    marginLeft: 10
  },
  icon: {
    textAlign: 'center',
    fontSize: 18
  }

});