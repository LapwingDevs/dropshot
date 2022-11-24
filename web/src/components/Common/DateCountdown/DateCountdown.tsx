import React, { Component, useEffect, useState } from 'react';

interface DateCountdownProps {
  deadline: string;
}

const DateCountdown = ({ deadline }: DateCountdownProps) => {
  const [days, setDays] = useState(0);
  const [hours, setHours] = useState(0);
  const [minutes, setMinutes] = useState(0);
  const [seconds, setSeconds] = useState(0);

  useEffect(() => {
    setInterval(() => getTimeUntil(deadline), 1000);
    return () => {
      getTimeUntil(deadline);
    };
  });

  const leading0 = (num: number) => {
    return num < 10 ? '0' + num : num;
  };

  const getTimeUntil = (deadline: string) => {
    const time = Date.parse(deadline) - new Date().getTime();
    if (time < 0) {
      setSeconds(0);
      setMinutes(0);
      setHours(0);
      setDays(0);
    } else {
      const seconds = Math.floor((time / 1000) % 60);
      const minutes = Math.floor((time / 1000 / 60) % 60);
      const hours = Math.floor((time / (1000 * 60 * 60)) % 24);
      const days = Math.floor(time / (1000 * 60 * 60 * 24));

      setSeconds(seconds);
      setMinutes(minutes);
      setHours(hours);
      setDays(days);
    }
  };

  return (
    <span>
      <span className="Clock-minutes">{leading0(minutes)}:</span>
      <span className="Clock-seconds">{leading0(seconds)}</span>
    </span>
  );
};
export default DateCountdown;
