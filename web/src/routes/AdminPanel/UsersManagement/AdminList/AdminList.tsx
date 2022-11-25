import React, { useCallback, useContext, useEffect, useState } from 'react';
import { getAdmins, promoteUser } from '../../../../api/controllers/AuthClient';
import { UserDto } from '../../../../api/models/User/UserDto';
import UserCard from '../../../../components/UserManagment/UserCard/UserCard';
import { degradeUser } from '../../../../api/controllers/AuthClient';
import AuthContext from '../../../../contexts/AuthContext';
import { useSnackbar } from 'notistack';

const AdminList = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { currentUser } = useContext(AuthContext);

  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [admins, setAdmins] = useState<UserDto[]>([]);

  const fetchAdmins = useCallback(async () => {
    const admins = await getAdmins();
    setAdmins(admins);

    setIsLoading(false);
  }, []);

  useEffect(() => {
    fetchAdmins();
  }, [fetchAdmins]);

  if (isLoading) {
    return <div>loading..</div>;
  }

  const isCurrentUser = (user: UserDto): boolean => {
    return currentUser !== undefined && currentUser.id == user.id;
  };

  const handleDegradeClicked = async (admin: UserDto) => {
    console.log('DEGRADE');
    try {
      await degradeUser(admin.email).then(() => {
        enqueueSnackbar('User successfully degraded', { variant: 'success' });
        setAdmins(admins.filter((item) => item !== admin));
      });
    } catch (e) {
      enqueueSnackbar('Error occurred!', { variant: 'error' });
    }
  };

  return (
    <div>
      <div>
        {admins.map((admin) => {
          return (
            <UserCard
              key={admin.id}
              buttonLabel="Degrade"
              onConfirm={() => handleDegradeClicked(admin)}
              user={admin}
              buttonDisabled={isCurrentUser(admin)}
            />
          );
        })}
      </div>
    </div>
  );
};

export default AdminList;
