import React, { useState, useCallback, useEffect, useMemo } from 'react';
import { Controller, useForm } from 'react-hook-form';
import './Login.scss';
import { Alert, Button, Container, TextField, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { passwordRegex } from '../../constants/Regexes';
import AuthContext from '../../contexts/AuthContext';
import { useSnackbar } from 'notistack';

const validationSchema = yup.object({
  email: yup.string().email('Incorrect email!').required('Email is required!'),
  password: yup
    .string()
    .required('Password is required')
    .min(8, 'Password must be longer than 8 characters!')
    .matches(passwordRegex, 'Password must contain uppercase letter, digit and special character!'),
});

interface IFormData {
  email: string;
  password: string;
}

const Login = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { signIn } = React.useContext(AuthContext);

  const defaultValue = useMemo<IFormData>(
    () => ({
      email: '',
      password: '',
    }),
    [],
  );

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<IFormData>({
    defaultValues: useMemo(() => {
      return defaultValue;
    }, [defaultValue]),
    resolver: yupResolver(validationSchema),
  });

  const navigate = useNavigate();

  const goToRegister = () => {
    navigate('/register');
  };

  const onSubmit = async (e: IFormData) => {
    try {
      await signIn(e.email, e.password);
      navigate('/drops');
    } catch (e) {
      enqueueSnackbar(`${e}`, { variant: 'error' });
    }
  };

  return (
    <div className="login-container">
      <Typography className="login-item" variant="h3">
        Log in to access hot drops in your neighbour!
      </Typography>

      <form>
        <div>
          <Controller
            name={'email'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="login-item"
                type="email"
                onChange={onChange}
                value={value}
                label={'Email'}
                helperText={errors.email?.message}
                error={!!errors.email}
              />
            )}
          />
        </div>

        <div>
          <Controller
            name={'password'}
            control={control}
            render={({ field: { onChange, value } }) => (
              <TextField
                className="login-item"
                type="password"
                onChange={onChange}
                value={value}
                label={'Password'}
                helperText={errors.password?.message}
                error={!!errors.password}
              />
            )}
          />
        </div>

        <div>
          <Button className="login-item" sx={{ width: '100%' }} type="submit" onClick={handleSubmit(onSubmit)}>
            Login
          </Button>
          <p>
            Dont have an account? <b onClick={goToRegister}>Register!</b>
          </p>
        </div>
      </form>
    </div>
  );
};

export default Login;
