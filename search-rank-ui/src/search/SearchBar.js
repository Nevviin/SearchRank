import React, { useState } from "react";
import * as helper from './helper';
const SearchBar = (props) => {
  //console.log("props.topResults", props.topResults);
  const [keyWords, setKeyWords] = useState("");
  const [url, setUrl] = useState("");
  const [topResults,setTopResults] = useState(props.topResults);
  const [engine,setEngine] = useState(props.searchEngine);
  const onKeyWordChange = (props) => {
    setKeyWords(props.target.value);
    console.log("keywords", props.target.value);
  };


  const searchEngineHandler =(event)=>{
    console.log('top results setSearchEngine changeHandler event',event.target.value);
    setEngine(event.target.value);
    console.log('top results setSearchEngine ',engine);
}
    
  const changeHandler =(event)=>{
    console.log('top results  changeHandler event',event.target.value);
    setTopResults(event.target.value);
    console.log('top results topResults ',topResults);
   
}
  const onUrlChange = (props) => {
    setUrl(props.target.value);
    console.log("setUrl", props.target.value);
  };

  const searchHandler = () => {
    
   if (helper.isEmpty(keyWords)){
      alert('keyWords field should not be empty');
      return false;
    }
    else if (helper.isEmpty(url) )
    {
      alert('url field should not be empty');
      return false;
    }
   var searchParam =  {searchEngine:engine,urlToSearch: url,keyWords: keyWords,noOfResults:topResults};
    props.callBack(searchParam)
  };


  const isValidHttpUrl = (string) => {
    let url;

    try {
      url = new URL(string);
    } catch (exception) {
        console.log('isValidHttpUrl exception',exception)
      return false;
    }

    return url.protocol === "http:" || url.protocol === "https:";
  };



  return (
  
      <div className="input-group input-group-sm mb-3">
              <span className="input-group-text" id="inputGroup-sizing-sm">
          Search Engine
        </span>
      <div style={{ width: "5.8em" }}>
            <select  value={engine}    onChange={searchEngineHandler} className="form-select"    id="searchEngine">
              <option value="google">google</option>
              <option value="bing">bing</option>
            </select>
      </div>
        <span className="input-group-text" id="inputGroup-sizing-sm">
          Top Search
        </span>
      <div style={{ width: "4.2em" }}>
            <select  value={topResults}    onChange={changeHandler} className="form-select"    id="specificSizeSelect">
              <option value="10">10</option>
              <option value="15">15</option>
              <option value="20">20</option>
              <option value="25">25</option>
              <option value="50">50</option>
              <option value="100">100</option>
            </select>
      </div>
        <span className="input-group-text" id="inputGroup-sizing-sm">
          KeyWords
        </span>
        <input
          type="text"
          name="url"
          value={keyWords}
          onChange={onKeyWordChange}
          className="form-control"
          aria-label="Sizing example input"
          aria-describedby="inputGroup-sizing-sm"
        />
          <span className="input-group-text" id="inputGroup-sizing-sm">
          Url
        </span>
             <input
          type="text"
          name="keyWords"
          value={url}
          onChange={onUrlChange}
          className="form-control"
          aria-label="Sizing example input"
          aria-describedby="inputGroup-sizing-sm"
        />
      
        <button
        className="btn btn-outline-primary"
        type="button"
        id="button-addon2"
        onClick={searchHandler}
      >
        Search
      </button>
      </div>

  
  
  );
};

export default SearchBar;
