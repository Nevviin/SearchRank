import React, { useState } from "react";
import "bootstrap/dist/css/bootstrap.css";
import * as helper from './helper';
import * as config from '../config/apiConfig';
import SearchBar from "./SearchBar";
import * as searchService from './searchService';

import SearchResult from "./SearchResult";
const Home = () => {
  const [showRankings, setShowRankings]=useState(false);
  const [rankings, setRankings]=useState('');

  const searchBarCallBack = (props) => {
    console.log("searchBarCallBack props", JSON.stringify(props));
    searchService.FetchSearchResults(props).then((response) => {
      if (response.status === 200) {
        
        if (helper.isNullOrEmpty(response.data))
        {
          setRankings('Sorry we are not able to find any ranking for the specified search');
        }
        else{setRankings(response.data);}
        setShowRankings(true);
        console.log("FetchSearchResults status 200 response", JSON.stringify(response));
      } else {
        console.log("FetchSearchResults status error", JSON.stringify(response.message));
      }
    });
  };
  
 



  return (
    <div className="container">
      <div className="row">
        <div className="col-sm" style={{ height: "2em" }}>
          {" "}
          {/*  this is to add vertical space  */}
        </div>
      </div>
      <div className="row">
  
        <div className="col-sm">
          <SearchBar  callBack = {searchBarCallBack}
           topResults={config.TOPRESULTS_DEFAULT} 
           searchEngine = {config.SEARCHENGINE_DEFAULT}
          ></SearchBar>
        </div>
        <div className="row">
          <div className="col-sm">
             {showRankings && <SearchResult  rankings={rankings} ></SearchResult> } 
        </div>
        </div>
      </div>
    </div>
  );
};
export default Home;
