import React, { useState } from "react";
import ReactDOM from 'react-dom';
import axios from "axios";
import swal from "sweetalert2";

export const FileUpload = () => {
    const [fileSelected, setFileSelected] = useState();

    const saveFileSelected = (e) => {
        setFileSelected(e.target.files[0]);
    };

    const importFile = async (e) => {
        const formData = new FormData();
        formData.append("file", fileSelected);
        try {
            var res = axios.post('https://localhost:44320/api/MeterReading/ProcessAndSaveReadings', formData, {
                headers: {
                    'content-type': 'application/json',
                    'Accept': 'application/json'
                }
            });
            swal.fire((await res).data)
      
        } catch (ex) {
            console.log(ex);
        }
    };

    return (
        <>
            <input type="file" accept=".csv, .xlsx, .xls" onChange={saveFileSelected} />
            <input type="button" value="upload" onClick={importFile} />
        </>


    );
};