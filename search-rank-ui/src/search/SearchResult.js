import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { faCheckCircle } from "@fortawesome/free-solid-svg-icons";
const SearchResult = (prop) => {
  return (
    <div className="card" >
      <div className="card-header">
        <div className="d-flex justify-content-center">
          <FontAwesomeIcon
            icon={faCheckCircle}
            color="green"
            size="2x"
          ></FontAwesomeIcon>
        </div>
      </div>
      <div className="card-body">
        <h5 className="card-title">Search Rankings</h5>
        <p className="card-text">
          Search ranking for the provided details are as below
        </p>
        <p>{prop.rankings}</p>
      </div>
    </div>
  );
};
export default SearchResult;
