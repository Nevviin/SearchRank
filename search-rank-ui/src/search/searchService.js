import axios from 'axios';
import * as config from '../config/apiConfig';


export const FetchSearchResults_Old = async (searchParam) => {
    console.log('FetchSearchResults function : ', JSON.stringify(searchParam));
    try {
        const response = await axios.post(config.SEARCH_ENDPOINT,
            searchParam,
            { headers: {'Content-Type': 'application/json'}});
            

        return response;
    } catch (exception) {
        return exception;
    }

}
export const FetchSearchResults = async (searchParam) => {
    console.log('FetchSearchResults searchParam ', JSON.stringify(searchParam));
    var url = config.RANK_ENDPOINT + `/${searchParam.searchEngine}` ;
    console.log('FetchSearchResults url : ', url);
    try {
        const response = await axios.post(url,
            searchParam,
            { headers: {'Content-Type': 'application/json'}});
            

        return response;
    } catch (exception) {
        return exception;
    }

}