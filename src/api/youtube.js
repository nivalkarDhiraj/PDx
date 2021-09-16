import axios from 'axios';
const KEY = 'AIzaSyAH1UI6LiNzDywC-rYNmn-zw-PxHrB3vK0'//'AIzaSyAMuLSXqnhiDOtrcrlnsPKioMOH8mxYItQ' //'AIzaSyAH1UI6LiNzDywC-rYNmn-zw-PxHrB3vK0' mention your youtube API key here

const baseURL= 'https://www.googleapis.com/youtube/v3/' //base url

//default url 
export default axios.create({
    baseURL: baseURL,
    params: {
        part: 'snippet',
        channelId:'UCL03ygcTgIbe36o2Z7sReuQ',
        maxResults: 50,
        key: KEY
    }
})



