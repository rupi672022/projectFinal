import { FlatList, View, Text, ImageBackground, TouchableOpacity, StyleSheet, Image } from 'react-native';
import React, { useState } from 'react';
import { Icon ,Overlay} from 'react-native-elements';
import image from '../Images/logo.jpg';
import imagelogo from '../Images/yakobs.png';

export default function OpenOrders(props) {

  //date
  var today = new Date(),
    date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
  const currentDate = date;

  const workerNum= props.route.params.workerNum ;

  const arrOrderEmploye = props.route.params.orderArr;//arr with the num order of worker

  const apiUrlOrder = "https://proj.ruppin.ac.il//igroup67/test2/tar6/api/Orders?idOrder";

  //Overlay
  const [visible, setVisible] = useState(false);
  const toggleOverlay = () => {
    setVisible(!visible);
  }


  const btngetproduct = (item) => {//get the product to the this order
    fetch(apiUrlOrder + "=" + item, {
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
          { props.navigation.navigate('Order', { num: item, arr: result, workerNum: workerNum }) }
        },
        (error) => {
          console.log("err post=", error);
        });

  }


  const btnDone = () => {//finish all the orders
    let arr = arrOrderEmploye.filter(obj => obj.Status != '0')
    if (arr.length == '0') {
      toggleOverlay();
    }
  }

  return (
    <View style={styles.view}>
      <ImageBackground source={image} resizeMode="cover" style={styles.imageback}>
        <Text style={styles.logo}>משק יעקבס</Text>
        <Text style={styles.dateText}>{currentDate}</Text>
        <Text style={styles.headText}>בחר הזמנה</Text>
      </ImageBackground>

      <Text style={styles.text}> מספרי הזמנות : </Text>

      <FlatList
        data={arrOrderEmploye}
        renderItem={({ item }) => (
          <View key={item} style={styles.listText}>
            <TouchableOpacity onPress={() => { btngetproduct(item.OrderNum) }}>
              <Text style={{
                flexDirection: 'row',
                marginLeft: 40,
                fontSize: 25,
                borderColor: item.Status == '1' ? '#efefef' : '#98FB98',
              }}>
                <Icon
                  name='check' size={30}
                  color={item.Status == '1' ? 'rgba(0,0,0,0)' : '#98FB98'}
                  style={{ marginTop: 10 }}
                />{item.OrderNum}</Text>
            </TouchableOpacity>
          </View>
        )}
      />


      <View>
        <TouchableOpacity onPress={btnDone}>
          <Text style={styles.buttonText}>שמור</Text>
        </TouchableOpacity>

        <Overlay isVisible={visible} onBackdropPress={toggleOverlay}>
          <Icon name='check' color='#98FB98' size={40}  />
          <Text style={{ textAlign: 'center', fontSize: 30 }}>יום העבודה הסתיים בהצלחה!!!</Text>
          <TouchableOpacity onPress={toggleOverlay}>
            <Text style={styles.buttonOvarlay}>אישור</Text>
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
    height: 400
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
    fontSize: 30,
    color: 'black',
    marginTop: -100,
    marginBottom: 10,
    marginLeft: 60,
    marginRight: 40,
    padding: 10,
    textAlign: 'center',
    borderColor: '#FFE587',

  },
  listText: {
    flexDirection: 'row',
    marginLeft: 90,
    padding: 5,
    fontSize: 30,
  },
  yakobsimg: {
    marginTop: 50,
    marginLeft: 10,
    marginBottom: 20,
    width: 70,
    height: 70
  },
  buttonText: {
    fontSize: 30,
    borderColor: '#FFE587',
    color: 'black',
    borderWidth: 2,
    borderRadius: 20,
    marginLeft: 110,
    marginRight:80,
    padding: 10,
    textAlign: 'center',
    marginBottom:-40
  },
  buttonOvarlay: {
    fontSize:30,
    marginTop: 10,
    textAlign: 'center',
    borderWidth: 2,
    backgroundColor: '#FFE587',
    width: 80,
    marginLeft: 110,
    borderColor: '#FFE587',
  },
  textOverly:{
    textAlign: 'center',
     fontSize: 30
  }

});