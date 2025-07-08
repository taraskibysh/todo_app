import React from "react";
import {LocalizationProvider} from "@mui/x-date-pickers/LocalizationProvider";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {StaticDatePicker} from "@mui/x-date-pickers/StaticDatePicker";

export const DeadlineCalendar: React.FC<{ deadline: string | undefined }> = ({ deadline }) => {
    if (!deadline) return <div> - </div>;

    const dateObj = new Date(deadline);

    return (
        <div>
            <div>
                {dateObj.toLocaleDateString('en-US', {
                    day: 'numeric',
                    month: 'long',
                    weekday: 'long',
                })}
            </div>
            <div>
                {dateObj.toLocaleTimeString('en-US', {
                    hour: 'numeric',
                    minute: 'numeric',
                    hour12: false

                })}
            </div>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <StaticDatePicker
                    displayStaticWrapperAs="desktop"
                    value={dateObj}
                    onChange={() => { }}
                    disabled
                    slots={{ toolbar: () => null }}
                />
            </LocalizationProvider>
        </div>
    );
};
